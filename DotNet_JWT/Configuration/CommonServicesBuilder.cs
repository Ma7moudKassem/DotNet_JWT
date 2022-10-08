namespace DotNet_JWT;

public class CommonServerBuilder
{
    public void Configure(WebApplicationBuilder builder)
    {
        builder.Services.DbContextConfigure(builder);
        builder.Services.JWTConfigure(builder);

        builder.Services.AddTransient<IAuthService, AuthService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        new MiddlewaresBuilder().ConfigureMiddleware(builder.Build());
    }
}
