using Common.Enums;
using Minio;
using Minio.DataModel.Args;

namespace API.Service;

public static partial class ServiceExtensions
{
    public static IApplicationBuilder InitializeMinio(this IApplicationBuilder app)
    {
        var directoryNames = Enum.GetNames(typeof(FileType));

        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var client = scope.ServiceProvider.GetService<IMinioClient>();

        foreach (var name in directoryNames)
        {
            var bucket = new BucketExistsArgs();
            bucket.WithBucket(name.ToLower());
            if (!client.BucketExistsAsync(bucket).GetAwaiter().GetResult())
            {
                var makeBucket = new MakeBucketArgs();
                makeBucket.WithBucket(name.ToLower());
                client.MakeBucketAsync(makeBucket).GetAwaiter().GetResult();
            }
        }

        return app;
    }
}