using Felipe.CleanArchitecture.Domain.Interfaces;
using Felipe.CleanArchitecture.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Felipe.CleanArchitecture.Infrastructure.Data.Repositories;

public abstract class BaseRepository<T>(AppDbContext dbContext) : IBaseRepository<T> where T : BaseAuditableEntity
{
    public IUnitOfWork UnitOfWork => dbContext;

    /// <inheritdoc/>
    public virtual Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.ModifiedDate = DateTime.UtcNow;
        dbContext.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>().RemoveRange(entities);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        return await dbContext.Set<T>().FindAsync([id], cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().ToListAsync(cancellationToken);
    }
}
