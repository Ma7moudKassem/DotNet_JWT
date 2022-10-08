//namespace DotNet_JWT;

//public class RegisterEntityConfiguration : IEntityTypeConfiguration<RegisterEntity>
//{
//    public void Configure(EntityTypeBuilder<RegisterEntity> builder)
//    {
//        builder.Property(e => e.Id).HasValueGenerator<GuidValueGenerator>();

//        builder.Property(e => e.Email).IsRequired();
//        builder.Property(e => e.Email).HasMaxLength(100);

//        builder.Property(e => e.UserName).IsRequired();
//        builder.Property(e => e.UserName).HasMaxLength(100);
        
//        builder.Property(e => e.FirstName).IsRequired();
//        builder.Property(e => e.FirstName).HasMaxLength(100);
        
//        builder.Property(e => e.LastName).IsRequired();
//        builder.Property(e => e.LastName).HasMaxLength(100);         

//        builder.Property(e => e.Password).IsRequired();
//        builder.Property(e => e.Password).HasMaxLength(20);
//    }
//}
