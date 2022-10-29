namespace DotNet_JWT;

public class RoleModel
{
    [Required] public Guid UserId { get; set; }
    [Required] public string Role { get; set; }
}
