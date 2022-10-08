namespace DotNet_JWT;

public interface IAuthService
{
    Task<AuthEntity> RegisterAsync(RegisterEntity registerEntity);
}
