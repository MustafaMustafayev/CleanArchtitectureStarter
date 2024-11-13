using Application.Dtos.Tokens;

namespace Application.Interfaces;
public interface ITokenService
{
    Task SoftDeleteTokenAsync(string accessToken);
    Task<TokenInfoDto> GenerateTokenAsync(Guid userId, string email);
    Task<bool> IsValidAccessTokenAsync(string accessToken);
}
