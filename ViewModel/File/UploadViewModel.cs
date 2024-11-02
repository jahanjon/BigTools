using Common.Enums;
using Microsoft.AspNetCore.Http;

namespace ViewModel.File;

public class UploadViewModel
{
    public IFormFile File { get; set; }
    public FileType Type { get; set; }
}