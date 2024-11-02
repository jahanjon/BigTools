using System.Security.Claims;
using Common.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("[area]/[controller]/[action]")]
public class BaseApiController : ControllerBase
{
    protected int UserId => HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
}