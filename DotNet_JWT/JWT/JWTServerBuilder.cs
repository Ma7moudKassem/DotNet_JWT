namespace DotNet_JWT;

public class JWTServerBuilder : IAPIBuilder
{
    private readonly WebApplicationBuilder _builder;
    public JWTServerBuilder(WebApplicationBuilder builder) => _builder = builder;
    public void Configure(IServiceCollection services)
    {
        services.Configure<JWT>(_builder.Configuration.GetSection("JWT"));

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _builder.Configuration["JWT:Issuer"],
                ValidAudience = _builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_builder.Configuration["JWT:Key"]))
            };
        });
    }
}
