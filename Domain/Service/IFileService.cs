using Common.Enums;
using Domain.Entity.File;
using Microsoft.AspNetCore.Http;

namespace Domain.Service;

public interface IFileService
{
    Task<ServiceResult<Guid>> UploadAsync(IFormFile file, FileType type, CancellationToken cancellationToken);
    Task<ServiceResult<bool>> UpdateAsync(Entity.File.File file, FileType newType, CancellationToken cancellationToken);
    Task<ServiceResult<string>> GetLinkAsync(Guid id, FileType type, CancellationToken cancellationToken);
}