using Microsoft.Extensions.Localization;
using Service;

namespace API.Service;

public static partial class ServiceExtensions
{
    public static void AddTranslate(this WebApplicationBuilder builder)
    {
        builder.Services.AddLocalization();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
    }
}