using Domain.Entities;

namespace Domain.Repositories;
public interface IErrorLogRepository
{
    Task AddAsync(ErrorLog errorLog);
    Task<(IEnumerable<ErrorLog>, int totalCount)> GetPaginatedListAsNoTrackingAsync(int pageNumber, int pageSize);
}