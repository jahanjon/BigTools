namespace Domain.Entity.Base;

public abstract class BaseEntity<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
    public bool Enabled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}