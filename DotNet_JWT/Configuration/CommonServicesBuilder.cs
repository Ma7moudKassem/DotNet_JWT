namespace DotNet_JWT;

public class CommonServerBuilder
{
    public void Configure(WebApplicationBuilder builder)
    {
        //builder.Services.DbContextConfigure(builder);
       // builder.Services.JWTConfigure(builder);

        IEnumerable<IAPIBuilder?> instances = Assembly.GetExecutingAssembly().GetTypes() 
        .Where(type => typeof(IAPIBuilder).IsAssignableFrom(type))
        .Where(type =>
            !type.IsAbstract &&
            !type.IsGenericType &&
             type.GetConstructor(new Type[0]) != null) 
        .Select(type => (IAPIBuilder?)Activator.CreateInstance(type)).ToList();

        foreach (IAPIBuilder? instance in instances)
        {
            instance?.Configure(builder.Services);
        }
        new JWTServerBuilder(builder).Configure(builder.Services);
        string connectionString = builder.Configuration.GetConnectionString("MyConnection");

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MigrationDbContext>();
        builder.Services.AddDbContext<MigrationDbContext>(e => e.UseSqlServer(connectionString));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        new MiddlewaresBuilder().ConfigureMiddleware(builder.Build());
    }
}
