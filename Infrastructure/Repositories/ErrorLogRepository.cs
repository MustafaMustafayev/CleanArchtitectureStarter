using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public sealed class ErrorLogRepository(AppDbContext dbContext) : IErrorLogRepository
{
    public async Task AddAsync(ErrorLog errorLog)
    {
        await dbContext.ErrorLogs.AddAsync(errorLog);
    }

    public async Task<(IEnumerable<ErrorLog>, int totalCount)> GetPaginatedListAsNoTrackingAsync(int pageNumber, int pageSize)
    {
        var query = dbContext.ErrorLogs;
        IEnumerable<ErrorLog> errorLogs = query.OrderByDescending(m => m.CreatedAt)
                                                .Skip((pageNumber - 1) * pageSize)
                                                .Take(pageSize);

        int totalCount = await query.CountAsync();

        return (errorLogs, totalCount);
    }
}