using Common.Utilities;
using DataAccess;
using Domain.Service.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Service;

public static partial class ServiceExtensions
{
    public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
    {
        Assert.NotNull(app, nameof(app));

        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

        var hasPendingModelChanges = dbContext?.Database.HasPendingModelChanges();

        if (hasPendingModelChanges.Value)
        {
            throw new ApplicationException("Cannot initialize database because the database is not updated.");
        }

        var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>();
        foreach (var dataInitializer in dataInitializers)
        {
            dataInitializer.InitializeData();
        }

        return app;
    }
}