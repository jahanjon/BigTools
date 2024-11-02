using Common.Enums;
using Domain;
using Domain.Repository.Base;
using Domain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Localization;
using Minio;
using Minio.DataModel.Args;
using File = Domain.Entity.File.File;

namespace Service;

public class FileService : IFileService
{
    private readonly IStringLocalizer<FileService> _localizer;
    private readonly IMinioClient _minioClient;
    private readonly IRepository<File> _repository;

    public FileService(IRepository<File> repository, IMinioClient minioClient, IStringLocalizer<FileService> localizer)
    {
        _repository = repository;
        _minioClient = minioClient;
        _localizer = localizer;
    }

    public async Task<ServiceResult<Guid>> UploadAsync(IFormFile file, FileType type,
        CancellationToken cancellationToken)
    {
        var fileGuid = Guid.NewGuid();
        var fileStream = new MemoryStream();
        await file.CopyToAsync(fileStream);
        var fileBytes = fileStream.ToArray();
        var fileExtension = Path.GetExtension(file.FileName);
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(type.ToString().ToLower())
            .WithObject($"{fileGuid}{fileExtension}")
            .WithStreamData(new MemoryStream(fileBytes))
            .WithObjectSize(fileStream.Length)
            .WithContentType("application/octet-stream");

        //validations
        var validateResult = await ValidateAsync(file, type, fileExtension, cancellationToken);
        if (!validateResult.IsSuccess)
            return new ServiceResult<Guid>(false, ApiResultStatusCode.BadRequest, Guid.Empty, validateResult.Message);

        var result = await _minioClient.PutObjectAsync(putObjectArgs);

        var newFile = new File
        {
            Id = fileGuid,
            Enabled = true,
            Type = type,
            FileExtension = fileExtension,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(newFile, cancellationToken);
        return new ServiceResult<Guid>(fileGuid);
    }

    public async Task<ServiceResult<string>> GetLinkAsync(Guid id, FileType type, CancellationToken cancellationToken)
    {
        var file = await _repository.GetByIdAsync(cancellationToken, id);
        var args = new PresignedGetObjectArgs()
            .WithBucket(type.ToString().ToLower())
            .WithObject($"{id}{file.FileExtension}")
            .WithExpiry(604800);

        var result = await _minioClient.PresignedGetObjectAsync(args);

        return new ServiceResult<string>(result);
    }


    public async Task<ServiceResult<bool>> UpdateAsync(File file, FileType newType, CancellationToken cancellationToken)
    {
        var srcArgs = new CopySourceObjectArgs()
            .WithBucket(file.Type.ToString().ToLower())
            .WithObject($"{file.Id}{file.FileExtension}");

        var args = new CopyObjectArgs()
            .WithBucket(newType.ToString().ToLower())
            .WithObject($"{file.Id}{file.FileExtension}")
            .WithCopyObjectSource(srcArgs);

        await _minioClient.CopyObjectAsync(args);

        return new ServiceResult<bool>(true);

    }

    private async Task<ServiceResult> ValidateAsync(IFormFile file, FileType type, string fileExtension, CancellationToken cancellationToken)
    {
        if (file.Length > 20971395)
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["MaxSizeLimit"]);
        fileExtension = fileExtension.Remove(0, 1);
        var fileType = GetTypeFromExtension(fileExtension);

        if (type == FileType.SupplierImage || type == FileType.SupplierMainImage || type == FileType.BrandLogo
            || type == FileType.ShopperImage || type == FileType.ShopperMainImage || type == FileType.Good
            || type == FileType.ShopperLicense || type == FileType.ShopperDocOrRent)
        {
            if (fileType != "image")
            {
                return new ServiceResult<Guid>(false, ApiResultStatusCode.BadRequest, Guid.Empty, _localizer["IncorrectFileType"]);
            }
        }
        if (type == FileType.SupplierVideo || type == FileType.SupplierMainVideo
            || type == FileType.ShopperVideo || type == FileType.ShopperMainVideo)
        {
            if (fileType != "video")
            {
                return new ServiceResult<Guid>(false, ApiResultStatusCode.BadRequest, Guid.Empty, _localizer["IncorrectFileType"]);
            }
        }
        if (type == FileType.BrandPriceList)
        {
            if (fileType != "image" && fileType != "Document")
                return new ServiceResult<Guid>(false, ApiResultStatusCode.BadRequest, Guid.Empty, _localizer["IncorrectFileType"]);
        }
        

        return new ServiceResult();
    }


    private string GetMimeType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out var contentType))
        {
            contentType = "application/octet-stream";
        }
        return contentType;
    }

    private string GetTypeFromExtension(string fileExtension)
    {
        var validImageExtensions = new List<string>()
        {
            "jpg", "jpeg", "jpe","png", "gif"
        };

        var validVideoExtensions = new List<string>()
        {
            "mkv", "mp4", "mov", "ogg", "avi", "3gp"
        };

        var validAudioExtensions = new List<string>()
        {
            "mp3", "m4a", "aac", "flac"
        };

        var validDocumentExtensions = new List<string>()
        {
            "pdf", "doc", "docx", "ppt", "pptx", "xls", "xlsx"
        };

        if (validImageExtensions.Contains(fileExtension))
            return "image";

        if (validVideoExtensions.Contains(fileExtension))
            return "video";

        if (validAudioExtensions.Contains(fileExtension))
            return "audio";

        if (validDocumentExtensions.Contains(fileExtension))
            return "Document";

        return "unknown";
    }


}