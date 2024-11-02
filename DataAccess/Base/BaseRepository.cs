using Common.Utilities;
using Domain.Entity.Base;
using Domain.Repository.Base;

namespace DataAccess.Base;

public class BaseRepository<TKey, TEntity> : Repository<TEntity>, IBaseRepository<TKey, TEntity>
    where TEntity : BaseEntity<TKey>
{
    public BaseRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public virtual async Task DisableAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));

        entity.UpdatedAt = DateTime.UtcNow;
        entity.Enabled = false;

        Entities.Update(entity);
        if (saveNow)
        {
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    #region Async Method

    public new virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        entity.CreatedAt = DateTime.UtcNow;
        await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        if (saveNow)
        {
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public new virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken,
        bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        foreach (var baseEntity in entities)
        {
            baseEntity.CreatedAt = DateTime.UtcNow;
        }

        await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        if (saveNow)
        {
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public new virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        entity.UpdatedAt = DateTime.UtcNow;
        Entities.Update(entity);
        if (saveNow)
        {
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public new virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken,
        bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        foreach (var baseEntity in entities)
        {
            baseEntity.UpdatedAt = DateTime.UtcNow;
        }

        Entities.UpdateRange(entities);
        if (saveNow)
        {
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    #endregion

    #region Sync Methods

    public new virtual void Add(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        entity.CreatedAt = DateTime.UtcNow;
        Entities.Add(entity);
        if (saveNow)
        {
            DbContext.SaveChanges();
        }
    }

    public new virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        foreach (var baseEntity in entities)
        {
            baseEntity.CreatedAt = DateTime.UtcNow;
        }

        Entities.AddRange(entities);
        if (saveNow)
        {
            DbContext.SaveChanges();
        }
    }

    public new virtual void Update(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        entity.UpdatedAt = DateTime.UtcNow;
        Entities.Update(entity);
        if (saveNow)
        {
            DbContext.SaveChanges();
        }
    }

    public new virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        foreach (var baseEntity in entities)
        {
            baseEntity.UpdatedAt = DateTime.UtcNow;
        }

        Entities.UpdateRange(entities);
        if (saveNow)
        {
            DbContext.SaveChanges();
        }
    }

    #endregion
}