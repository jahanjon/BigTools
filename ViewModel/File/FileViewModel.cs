using Common.Enums;

namespace ViewModel.File;

public class FileViewModel
{
    public Guid? FileId { get; set; }
    public string Link { get; set; }
    public FileType? FileType { get; set; }
}