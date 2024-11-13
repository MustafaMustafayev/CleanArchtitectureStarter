using Application.Interfaces;
using Domain.Repositories;
using Infrastructure.DatabaseContexts;
using System.Threading;

namespace Infrastructure.Repositories;
public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}
