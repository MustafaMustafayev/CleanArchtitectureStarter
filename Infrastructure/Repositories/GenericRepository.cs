using Domain.Primitives;
using Domain.Repositories;
using Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public sealed class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _ctx;
    public GenericRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<TEntity> AddAsync(TEntity entry)
    {
        var a = await _ctx.AddAsync(entry);
        return entry;
    }

    public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entries)
    {
        await _ctx.AddRangeAsync(entries);
        return entries;
    }

    public void Delete(TEntity entry)
    {
        _ctx.Remove(entry);
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
        _ctx.Update(entry);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await _ctx.Set<TEntity>().IgnoreQueryFilters().FirstOrDefaultAsync(filter)
            : await _ctx.Set<TEntity>().FirstOrDefaultAsync(filter);
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false)
    {
        return filter is null
            ? ignoreQueryFilters
                ? await _ctx.Set<TEntity>().IgnoreQueryFilters().ToListAsync()
                : await _ctx.Set<TEntity>().ToListAsync()
            : ignoreQueryFilters
                ? await _ctx.Set<TEntity>().Where(filter).IgnoreQueryFilters().ToListAsync()
                : await _ctx.Set<TEntity>().Where(filter).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetListAsNoTrackingAsync(Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false)
    {
        return filter is null
            ? ignoreQueryFilters
                ? await _ctx.Set<TEntity>().IgnoreQueryFilters().AsNoTracking().ToListAsync()
                : await _ctx.Set<TEntity>().AsNoTracking().ToListAsync()
            : ignoreQueryFilters
                ? await _ctx.Set<TEntity>().Where(filter).IgnoreQueryFilters().AsNoTracking().ToListAsync()
                : await _ctx.Set<TEntity>().Where(filter).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetListAsNoTrackingWithIdentityResolutionAsync(Expression<Func<TEntity, bool>>? filter = null, bool ignoreQueryFilters = false)
    {
        return filter is null
            ? ignoreQueryFilters
                ? await _ctx.Set<TEntity>().IgnoreQueryFilters().AsNoTrackingWithIdentityResolution().ToListAsync()
                : await _ctx.Set<TEntity>().AsNoTrackingWithIdentityResolution().ToListAsync()
            : ignoreQueryFilters
                ? await _ctx.Set<TEntity>().Where(filter).IgnoreQueryFilters().AsNoTrackingWithIdentityResolution().ToListAsync()
                : await _ctx.Set<TEntity>().Where(filter).AsNoTrackingWithIdentityResolution().ToListAsync();
    }

    public async Task<TEntity?> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _ctx.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter);
    }

    public async Task<TEntity?> GetAsNoTrackingWithIdentityResolutionAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _ctx.Set<TEntity>().AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(filter);
    }

    public async Task<TEntity> UpdateAsync(TEntity entry)
    {
        _ctx.Update(entry);
        return await Task.FromResult(entry);
    }

    public async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entries)
    {
        _ctx.UpdateRange(entries);
        return await Task.FromResult(entries);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await _ctx.Set<TEntity>().IgnoreQueryFilters().CountAsync(filter)
            : await _ctx.Set<TEntity>().CountAsync(filter);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await _ctx.Set<TEntity>().IgnoreQueryFilters().AnyAsync(filter)
            : await _ctx.Set<TEntity>().AnyAsync(filter);
    }

    public async Task<bool> AllAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await _ctx.Set<TEntity>().IgnoreQueryFilters().AllAsync(filter)
            : await _ctx.Set<TEntity>().AllAsync(filter);
    }

    public async Task<TEntity> GetAsync(Guid id)
    {
        return (await _ctx.Set<TEntity>().FindAsync(id))!;
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
        bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await _ctx.Set<TEntity>().IgnoreQueryFilters().SingleOrDefaultAsync(filter)
            : await _ctx.Set<TEntity>().SingleOrDefaultAsync(filter);
    }

    public async Task<TEntity?> SingleAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await _ctx.Set<TEntity>().IgnoreQueryFilters().SingleAsync(filter)
            : await _ctx.Set<TEntity>().SingleAsync(filter);
    }

    public async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> filter, bool ignoreQueryFilters = false)
    {
        return ignoreQueryFilters
            ? await _ctx.Set<TEntity>().IgnoreQueryFilters().FirstAsync(filter)
            : await _ctx.Set<TEntity>().FirstAsync(filter);
    }

    public void DeleteRange(List<TEntity> entries)
    {
        _ctx.RemoveRange(entries);
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
            _ctx.Update(entry);
        }
    }
}