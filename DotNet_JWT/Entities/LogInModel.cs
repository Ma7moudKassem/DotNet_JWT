using Microsoft.Build.Framework;

namespace DotNet_JWT;

public class LogInModel
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }
}
