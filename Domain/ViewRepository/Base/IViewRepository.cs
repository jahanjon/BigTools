using System.Linq.Expressions;
using Domain.ViewEntity.Base;

namespace Domain.ViewRepository.Base;

public interface IViewRepository<TEntity> where TEntity : IViewEntity
{
    IQueryable<TEntity> TableNoTracking { get; }
    TEntity GetById(params object[] ids);
    ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
        where TProperty : class;

    Task LoadCollectionAsync<TProperty>(TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
        where TProperty : class;

    void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
        where TProperty : class;

    Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty,
        CancellationToken cancellationToken) where TProperty : class;
}