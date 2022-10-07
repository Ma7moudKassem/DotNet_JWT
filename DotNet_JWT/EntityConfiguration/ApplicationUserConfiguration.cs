namespace DotNet_JWT;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(e => e.FirstName).IsRequired();
        builder.Property(e => e.FirstName).HasMaxLength(50);

        builder.Property(e => e.LastName).IsRequired();
        builder.Property(e => e.LastName).HasMaxLength(50);
    }
}
