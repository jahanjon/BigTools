using Common.Settings;
using Domain.Service;
using Minio;
using Service;

namespace API.Service;

public static partial class ServiceExtensions
{
    public static void AddMinio(this WebApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSection(nameof(MinioSettings)).Get<MinioSettings>();

        builder.Services.AddMinio(x =>
        {
            x.WithCredentials(settings.AccessKey, settings.SecretKey);
            x.WithEndpoint(new Uri(settings.Url));
            x.WithSSL(builder.Environment.IsProduction());
        });

        builder.Services.AddScoped<IFileService, FileService>();
    }
}