using Domain.Entity.Base;

namespace Domain.Repository.Base;

public interface IBaseRepository<TKey, T> : IRepository<T> where T : BaseEntity<TKey>
{
    Task DisableAsync(T entity, CancellationToken cancellationToken, bool saveNow = true);
}