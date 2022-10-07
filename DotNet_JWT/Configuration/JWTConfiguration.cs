namespace DotNet_JWT;

public static class JWTConfiguration
{
    public static void JWTConfigure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
    }
}
