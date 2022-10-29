namespace DotNet_JWT;

public interface IAuthService
{
    Task<AuthEntity> RegisterAsync(RegisterEntity registerEntity);
    Task<AuthEntity> LogInAsync(LogInModel logInModel);
    Task<string> AddRoleAsync(RoleModel role);
}
