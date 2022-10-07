namespace DotNet_JWT;

public class ServiceBuilder : IServiceBuilder
{
    public virtual void Configure(IServiceCollection services , WebApplicationBuilder builder)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
