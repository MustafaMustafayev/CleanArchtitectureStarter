using System.Linq.Expressions;

namespace Domain.Repositories;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? filter = null, bool ignoreQueryFilters = false);
    Task<IEnumerable<T>> GetListAsNoTrackingAsync(Expression<Func<T, bool>>? filter = null, bool ignoreQueryFilters = false);
    Task<IEnumerable<T>> GetListAsNoTrackingWithIdentityResolutionAsync(Expression<Func<T, bool>>? filter = null, bool ignoreQueryFilters = false);
    Task<(IEnumerable<T>, int totalCount)> GetPaginatedListAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter = null, bool ignoreQueryFilters = false);
    Task<(IEnumerable<T>, int totalCount)> GetPaginatedListAsNoTrackingAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter = null, bool ignoreQueryFilters = false);
    Task<(IEnumerable<T>, int totalCount)> GetPaginatedListAsNoTrackingWithIdentityResolutionAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter = null, bool ignoreQueryFilters = false);
    Task<T?> GetAsync(Expression<Func<T, bool>> filter, bool ignoreQueryFilters = false);
    Task<T> GetAsync(Guid id);
    Task<T?> GetAsNoTrackingAsync(Expression<Func<T, bool>> filter);
    Task<T?> GetAsNoTrackingWithIdentityResolutionAsync(Expression<Func<T, bool>> filter);
    Task<int> CountAsync(Expression<Func<T, bool>> filter, bool ignoreQueryFilters = false);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool ignoreQueryFilters = false);
    Task<bool> AllAsync(Expression<Func<T, bool>> filter, bool ignoreQueryFilters = false);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> filter, bool ignoreQueryFilters = false);
    Task<T?> SingleAsync(Expression<Func<T, bool>> filter, bool ignoreQueryFilters = false);
    Task<T?> FirstAsync(Expression<Func<T, bool>> filter, bool ignoreQueryFilters = false);
    Task<T> AddAsync(T entry);
    Task<List<T>> AddRangeAsync(List<T> entries);
    Task<T> UpdateAsync(T entry);
    Task<List<T>> UpdateRangeAsync(List<T> entries);
    void Delete(T entry);
    void DeleteRange(List<T> entries);
    void SoftDelete(T entry);
    void SoftDeleteRange(List<T> entries);
}