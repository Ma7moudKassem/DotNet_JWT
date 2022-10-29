namespace DotNet_JWT;

public class AuthServerBuilder : IAPIBuilder
{
    public void Configure(IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
    }
}
