using Common.Enums;
using Domain.Entity.Base;

namespace Domain.Entity.File;

public class File : BaseEntity<Guid>
{
    public FileType Type { get; set; }
    public string FileExtension { get; set; }
}