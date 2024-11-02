using API.Middleware;
using API.Service;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddSettings();
        builder.AddLogger();
        builder.AddDomainMapper();
        builder.AddModelMapper();
        builder.AddAPIMapper();
        builder.AddDbContext();
        builder.AddIdentity();
        builder.AddRepository();
        builder.AddService();
        builder.AddJwtAuthentication();
        builder.AddMinio();
        builder.AddTranslate();
        builder.AddHttpCommunication();

        builder.Services.AddControllers().AddNewtonsoftJson();

        builder.Services.AddEndpointsApiExplorer();
        builder.AddSwagger();

        var app = builder.Build();

        app.InitializeDatabase();
        app.InitializeMinio();
        app.UseMiddleware<CustomExceptionHandlerMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHsts();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials());

        app.Run();
    }
}