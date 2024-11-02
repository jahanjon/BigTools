using Domain.Entity.Identity;

namespace API.Service;

public static partial class ServiceExtensions
{
    public static void AddModelMapper(this WebApplicationBuilder builder)
    {
        var aPIMapersAssembly = typeof(User).Assembly;
        builder.Services.AddAutoMapper(aPIMapersAssembly);
    }

    public static void AddAPIMapper(this WebApplicationBuilder builder)
    {
        var aPIMapersAssembly = typeof(Program).Assembly;
        builder.Services.AddAutoMapper(aPIMapersAssembly);
    }
}