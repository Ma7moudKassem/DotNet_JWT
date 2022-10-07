namespace DotNet_JWT;

public class CommonServerBuilder
{
    public void Configure(WebApplicationBuilder builder, string appConnectionString)
    {

        string connectionString = builder.Configuration.GetConnectionString(appConnectionString);

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MigrationDbContext>();
        builder.Services.AddDbContext<MigrationDbContext>(e => e.UseSqlServer(connectionString));

        builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

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
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
