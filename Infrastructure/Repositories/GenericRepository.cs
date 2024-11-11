using Domain.Pagination;
using Domain.Primitives;
using Domain.Repositories;
using Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class GenericRepository<TEntity>(AppDbContext dbContext) : IGenericRepository<TEntity> where TEntity : class
{
    public async Task<TEntity> AddAsync(TEntity entry)
    {
        await dbContext.AddAsync(entry);
        return entry;
    }

    public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entries)
    {
        await dbContext.AddRangeAsync(entries);
        return entries;
    }

    public void Delete(TEntity entry)
    {
        dbContext.Remove(entry);
    }

    public void SoftDelete(TEntity entry)
    {
        var property = entry.GetType().GetProperty(nameof(Auditable.IsDeleted)) ?? throw new ArgumentException(
                @$"The property with type: {entry.GetType()} can not be SoftDeleted, 
                        because it doesn't contains {nameof(Auditable.IsDeleted)} property, 
                        and did not implemented {typeof(Auditable)}.");
        if (((bool?)property.GetValue(entry)!).Value)
        {
            throw new Exception("This entity was already deleted.");
        }

        property.SetValue(entry, true);
        dbContext.Update(entry);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().FirstOrDefaultAsync(filter)
            : await dbContext.Set<TEntity>().FirstOrDefaultAsync(filter);
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false)
    {
        return filter is null
            ? ignoreQueryFilters
                ? await dbContext.Set<TEntity>().IgnoreQueryFilters().ToListAsync()
                : await dbContext.Set<TEntity>().ToListAsync()
            : ignoreQueryFilters
                ? await dbContext.Set<TEntity>().Where(filter).IgnoreQueryFilters().ToListAsync()
                : await dbContext.Set<TEntity>().Where(filter).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetListAsNoTrackingAsync(Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false)
    {
        return filter is null
            ? ignoreQueryFilters
                ? await dbContext.Set<TEntity>().IgnoreQueryFilters().AsNoTracking().ToListAsync()
                : await dbContext.Set<TEntity>().AsNoTracking().ToListAsync()
            : ignoreQueryFilters
                ? await dbContext.Set<TEntity>().Where(filter).IgnoreQueryFilters().AsNoTracking().ToListAsync()
                : await dbContext.Set<TEntity>().Where(filter).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetListAsNoTrackingWithIdentityResolutionAsync(Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false)
    {
        return filter is null
            ? ignoreQueryFilters
                ? await dbContext.Set<TEntity>().IgnoreQueryFilters().AsNoTrackingWithIdentityResolution().ToListAsync()
                : await dbContext.Set<TEntity>().AsNoTrackingWithIdentityResolution().ToListAsync()
            : ignoreQueryFilters
                ? await dbContext.Set<TEntity>().Where(filter).IgnoreQueryFilters().AsNoTrackingWithIdentityResolution().ToListAsync()
                : await dbContext.Set<TEntity>().Where(filter).AsNoTrackingWithIdentityResolution().ToListAsync();
    }

    public async Task<TEntity?> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter);
    }

    public async Task<TEntity?> GetAsNoTrackingWithIdentityResolutionAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await dbContext.Set<TEntity>().AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(filter);
    }

    public async Task<TEntity> UpdateAsync(TEntity entry)
    {
        dbContext.Update(entry);
        return await Task.FromResult(entry);
    }

    public async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entries)
    {
        dbContext.UpdateRange(entries);
        return await Task.FromResult(entries);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().CountAsync(filter)
            : await dbContext.Set<TEntity>().CountAsync(filter);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().AnyAsync(filter)
            : await dbContext.Set<TEntity>().AnyAsync(filter);
    }

    public async Task<bool> AllAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().AllAsync(filter)
            : await dbContext.Set<TEntity>().AllAsync(filter);
    }

    public async Task<TEntity> GetAsync(Guid id)
    {
        return (await dbContext.Set<TEntity>().FindAsync(id))!;
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
        bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().SingleOrDefaultAsync(filter)
            : await dbContext.Set<TEntity>().SingleOrDefaultAsync(filter);
    }

    public async Task<TEntity?> SingleAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().SingleAsync(filter)
            : await dbContext.Set<TEntity>().SingleAsync(filter);
    }

    public async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await dbContext.Set<TEntity>().IgnoreQueryFilters().FirstAsync(filter)
            : await dbContext.Set<TEntity>().FirstAsync(filter);
    }

    public void DeleteRange(List<TEntity> entries)
    {
        dbContext.RemoveRange(entries);
    }

    public void SoftDeleteRange(List<TEntity> entries)
    {
        foreach(TEntity entry in entries)
        {
            var property = entry.GetType().GetProperty(nameof(Auditable.IsDeleted)) ?? throw new ArgumentException(
                        @$"The property with type: {entry.GetType()} can not be SoftDeleted, 
                        because it doesn't contains {nameof(Auditable.IsDeleted)} property, 
                        and did not implemented {typeof(Auditable)}.");
            if (((bool?)property.GetValue(entry)!).Value)
            {
                throw new Exception("This entity was already deleted.");
            }

            property.SetValue(entry, true);
            dbContext.Update(entry);
        }
    }

    public async Task<PaginatedResult<TEntity>> GetPaginatedListAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false)
    {
        var query = filter is null
            ? ignoreQueryFilters
                ? dbContext.Set<TEntity>().IgnoreQueryFilters()
                : dbContext.Set<TEntity>()
            : ignoreQueryFilters
                ? dbContext.Set<TEntity>().Where(filter).IgnoreQueryFilters()
                : dbContext.Set<TEntity>().Where(filter);

        var datas = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var totalCount = await query.CountAsync();

        return new PaginatedResult<TEntity>(datas, totalCount, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<TEntity>> GetPaginatedListAsNoTrackingAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false)
    {
        var query = filter is null
            ? ignoreQueryFilters
                ? dbContext.Set<TEntity>().IgnoreQueryFilters().AsNoTracking()
                : dbContext.Set<TEntity>().AsNoTracking()
            : ignoreQueryFilters
                ? dbContext.Set<TEntity>().Where(filter).IgnoreQueryFilters().AsNoTracking()
                : dbContext.Set<TEntity>().Where(filter).AsNoTracking();

        var datas = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var totalCount = await query.CountAsync();

        return new PaginatedResult<TEntity>(datas, totalCount, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<TEntity>> GetPaginatedListAsNoTrackingWithIdentityResolutionAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false)
    {
        var query = filter is null
            ? ignoreQueryFilters
                ? dbContext.Set<TEntity>().IgnoreQueryFilters().AsNoTrackingWithIdentityResolution()
                : dbContext.Set<TEntity>().AsNoTrackingWithIdentityResolution()
            : ignoreQueryFilters
                ? dbContext.Set<TEntity>().Where(filter).IgnoreQueryFilters().AsNoTrackingWithIdentityResolution()
                : dbContext.Set<TEntity>().Where(filter).AsNoTrackingWithIdentityResolution();

        var datas = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var totalCount = await query.CountAsync();

        return new PaginatedResult<TEntity>(datas, totalCount, pageNumber, pageSize);
    }
}