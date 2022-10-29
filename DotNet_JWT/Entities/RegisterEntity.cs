using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DotNet_JWT;

public class RegisterEntity
{
    [Required, MaxLength(100)] public string FirstName { get; set; }
    [Required, MaxLength(100)] public string LastName { get; set; }
    [Required, MaxLength(100)] public string UserName { get; set; }
    [Required, MaxLength(100)] public string Email { get; set; }
    [Required, MaxLength(100)] public string Password { get; set; }
}
