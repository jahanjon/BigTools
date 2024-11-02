using System.Linq.Expressions;
using Common.Utilities;
using Domain.ViewEntity.Base;
using Domain.ViewRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Base;

public class ViewRepository<TEntity> : IViewRepository<TEntity> where TEntity : class, IViewEntity
{
    protected readonly AppDbContext DbContext;

    public ViewRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>(); // City => Cities
    }

    public DbSet<TEntity> Entities { get; }

    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    #region Async Method

    public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return Entities.FindAsync(ids, cancellationToken);
    }

    #endregion

    #region Sync Methods

    public virtual TEntity GetById(params object[] ids)
    {
        return Entities.Find(ids);
    }

    #endregion

    #region Attach & Detach

    public virtual void Detach(TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        var entry = DbContext.Entry(entity);
        if (entry != null)
        {
            entry.State = EntityState.Detached;
        }
    }

    public virtual void Attach(TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        if (DbContext.Entry(entity).State == EntityState.Detached)
        {
            Entities.Attach(entity);
        }
    }

    #endregion

    #region Explicit Loading

    public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
        where TProperty : class
    {
        Attach(entity);

        var collection = DbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded)
        {
            await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public virtual void LoadCollection<TProperty>(TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
        where TProperty : class
    {
        Attach(entity);
        var collection = DbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded)
        {
            collection.Load();
        }
    }

    public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity,
        Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
        where TProperty : class
    {
        Attach(entity);
        var reference = DbContext.Entry(entity).Reference(referenceProperty);
        if (!reference.IsLoaded)
        {
            await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
        where TProperty : class
    {
        Attach(entity);
        var reference = DbContext.Entry(entity).Reference(referenceProperty);
        if (!reference.IsLoaded)
        {
            reference.Load();
        }
    }

    #endregion
}