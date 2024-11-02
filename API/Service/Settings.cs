using Common.Settings;

namespace API.Service;

public static partial class ServiceExtensions
{
    public static void AddSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));
        builder.Services.Configure<SMSSettings>(builder.Configuration.GetSection(nameof(SMSSettings)));
    }
}