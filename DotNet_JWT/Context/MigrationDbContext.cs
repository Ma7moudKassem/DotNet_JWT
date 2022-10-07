namespace DotNet_JWT;

public class MigrationDbContext : IdentityDbContext<ApplicationUser>
{
    public MigrationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
