using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace API.Service;

public static partial class ServiceExtensions
{
    public static void AddDbContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, dbConfig =>
            {
                dbConfig.CommandTimeout(30);
                dbConfig.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });
    }
}