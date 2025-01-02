using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public sealed class TokenRepository(AppDbContext dbContext) : ITokenRepository
{
    public async Task AddAsync(Token token)
    {
        await dbContext.Tokens.AddAsync(token);
    }

    public async Task<bool> IsValidAccessTokenAsync(string accessToken)
    {
        return await dbContext.Tokens.AnyAsync(m => m.AccessToken == accessToken &&
                                                    m.AccessTokenExpireDate >= DateTime.Now);
    }

    public async Task SoftDeleteAsync(string accessToken)
    {
        Token? token = await dbContext.Tokens.FirstOrDefaultAsync(m => m.AccessToken == accessToken);

        if (token is { })
        {
            token.IsDeleted = true;
        }
    }
}