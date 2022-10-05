namespace DotNet_JWT
{
    public class MigrationDbContext : GenericContext
    {
        public MigrationDbContext(DbContextOptions options) : base(options) { }
    }
}
