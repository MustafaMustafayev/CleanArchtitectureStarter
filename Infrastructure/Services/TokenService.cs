using Application.Dtos.Tokens;
using Application.Interfaces;
using Application.Settings;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;
public sealed class TokenService(ITokenRepository tokenRepository,
                                 IUnitOfWork unitOfWork,
                                 IEncryptionService encryptionService,
                                 ConfigSettings configSettings) : ITokenService
{
    async Task AddAsync(Token token)
    {
        await tokenRepository.AddAsync(token);
    }

    public async Task<TokenInfoDto> GenerateTokenAsync(Guid userId, string email)
    {
        TokenInfoDto tokenInfo = CreateToken(userId, email);

        Token token = new()
        {
            UserId = userId,
            AccessToken = tokenInfo.AccessToken,
            AccessTokenExpireDate = tokenInfo.AccessTokenExpireDate
        };

        await AddAsync(token);

        return tokenInfo;
    }

    TokenInfoDto CreateToken(Guid userId, string email)
    {
        DateTime expirationDate = DateTime.Now.AddHours(configSettings.AuthSettings.TokenExpirationTimeInHours);

        var claims = new List<Claim>
        {
        new(configSettings.AuthSettings.TokenUserIdKey, encryptionService.Encrypt(userId.ToString())),
        new(ClaimTypes.Email, email),
        new(ClaimTypes.Expiration, expirationDate.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configSettings.AuthSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expirationDate,
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        string jwtToken = tokenHandler.WriteToken(token);

        TokenInfoDto tokenInfoDto = new()
        {
            AccessToken = jwtToken,
            AccessTokenExpireDate = expirationDate
        };

        return tokenInfoDto;
    }

    public async Task SoftDeleteTokenAsync(string accessToken)
    {
        await tokenRepository.SoftDeleteAsync(accessToken);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> IsValidAccessTokenAsync(string accessToken)
    {
        return await tokenRepository.IsValidAccessTokenAsync(accessToken);
    }
}