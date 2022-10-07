namespace DotNet_JWT;

public interface IServiceBuilder
{
    public void Configure(IServiceCollection services , WebApplicationBuilder builder);
}
