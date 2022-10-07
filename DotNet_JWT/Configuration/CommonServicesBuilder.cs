namespace DotNet_JWT;

public class CommonServerBuilder
{
    public void Configure(WebApplicationBuilder builder)
    {
        builder.Services.DbContextConfigure(builder);

        builder.Services.JWTConfigure(builder);

        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        WebApplication app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<MigrationDbContext>();
            dataContext.Database.Migrate();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
