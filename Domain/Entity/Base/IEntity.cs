namespace Domain.Entity.Base;

public interface IEntity
{
}

public interface IEntity<TKey> : IEntity
{
    public TKey Id { get; set; }
    public bool Enabled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}