using Domain.Entity.Base;

namespace Domain.Entity.File;

public class FileRelation : BaseEntity
{
    public int EntityId { get; set; }
    public string EntityTypeName { get; set; }
    public Guid FileId { get; set; }
    public File File { get; set; }
}