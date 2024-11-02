using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel.File;

namespace API.Areas.File.Controllers;

public class FileController : BaseFileController
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Supplier,Shopper")]
    public async Task<IActionResult> UploadAsync([FromForm] UploadViewModel viewModel, CancellationToken cancellationToken)
    {
        var response = await _fileService.UploadAsync(viewModel.File, viewModel.Type, cancellationToken);

        return new AppHttpResponse<Guid>(response).Create(response.Data);
    }
}