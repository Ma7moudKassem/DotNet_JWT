namespace DotNet_JWT;

public class ContextServerBuilder : IAPIBuilder
{
    private readonly WebApplicationBuilder _builder;

    public ContextServerBuilder(WebApplicationBuilder builder) => _builder = builder;

    public void Configure(IServiceCollection services)
    {
        string connectionString = _builder.Configuration.GetConnectionString("MyConnection");

        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MigrationDbContext>();
        services.AddDbContext<MigrationDbContext>(e => e.UseSqlServer(connectionString));
    }
}
