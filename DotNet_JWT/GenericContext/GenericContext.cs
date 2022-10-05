namespace DotNet_JWT;

public class GenericContext : IdentityDbContext<IdentityUser>
{
    public GenericContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
