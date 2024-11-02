namespace API.Service;

public static partial class ServiceExtensions
{
    public static void AddHttpCommunication(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient("RayganSMS", httpClient => { httpClient.BaseAddress = new Uri("http://smspanel.Trez.ir/"); });
    }
}