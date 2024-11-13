using Domain.Entities;

namespace Domain.Repositories;
public interface ITokenRepository
{
    Task AddAsync(Token token);
    Task SoftDeleteAsync(string accessToken);
    Task<bool> IsValidAccessTokenAsync(string accessToken);
}
