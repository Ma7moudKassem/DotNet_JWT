namespace DotNet_JWT;

public static class DbContextConfiguration
{
    public static void DbContextConfigure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetConnectionString("MyConnection");

        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MigrationDbContext>();
        services.AddDbContext<MigrationDbContext>(e => e.UseSqlServer(connectionString));
    }
}
