using Common.Enums;

namespace Domain.Dto.File;

public class FileDto
{
    public Guid? FileId { get; set; }
    public string Link { get; set; }
    public FileType? FileType { get; set; }
}