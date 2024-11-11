using Application.Interfaces;
using Domain.Repositories;
using Infrastructure.DatabaseContexts;

namespace Infrastructure.Repositories;
public class UnitOfWork(AppDbContext dbContext, 
                        ITokenResolverService tokenResolverService) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
