using Application.Helpers;
using Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Helpers;
public sealed class TokenResolverService(IEncryptionService encryptionService,
                                         IHttpContextAccessor httpContextAccessor,
                                         ConfigSettings configSettings) : ITokenResolverService
{
    public Guid? GetUserIdFromToken()
    {
        var token = GetJwtSecurityToken();
        var userId = encryptionService.Decrypt(token.Claims.First(c => c.Type == configSettings.AuthSettings.TokenUserIdKey).Value);
        return Guid.Parse(userId);
    }

    private JwtSecurityToken GetJwtSecurityToken()
    {
        var tokenString = GetTokenString();
        return new JwtSecurityToken(tokenString[7..]);
    }

    public string GetTokenString()
    {
        return httpContextAccessor.HttpContext?.Request.Headers[configSettings.AuthSettings.HeaderName].ToString()!;
    }
}
