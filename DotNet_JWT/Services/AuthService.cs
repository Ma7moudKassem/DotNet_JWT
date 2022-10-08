using Serilog;

namespace DotNet_JWT;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JWT _jwt;
    public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt)
    {
        _userManager = userManager;
        _jwt = jwt.Value;
    }

    public async Task<AuthEntity> RegisterAsync(RegisterEntity registerEntity)
    {
        try
        {
            if (await _userManager.FindByEmailAsync(registerEntity.Email) is not null)
                return new AuthEntity { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(registerEntity.UserName) is not null)
                return new AuthEntity { Message = "User name is already registered!" };

            ApplicationUser user = new ApplicationUser
            {
                Email = registerEntity.Email,
                UserName = registerEntity.UserName,
                FirstName = registerEntity.FirstName,
                LastName = registerEntity.LastName,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerEntity.Password);

            if (!result.Succeeded)
            {
                string errors = string.Empty;

                foreach (IdentityError error in result.Errors)
                {
                    errors += $"{error.Description},";
                }

                return new AuthEntity { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);

            return new AuthEntity
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName
            };
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            throw;
        };
    }
    //public async Task<AuthEntity> GetTokenAsync(TokenRequestModel model)
    //{
    //    var AuthEntity = new AuthEntity();

    //    var user = await _userManager.FindByEmailAsync(model.Email);

    //    if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
    //    {
    //        AuthEntity.Message = "Email or Password is incorrect!";
    //        return AuthEntity;
    //    }

    //    var jwtSecurityToken = await CreateJwtToken(user);
    //    var rolesList = await _userManager.GetRolesAsync(user);

    //    AuthEntity.IsAuthenticated = true;
    //    AuthEntity.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    //    AuthEntity.Email = user.Email;
    //    AuthEntity.UserName = user.UserName;
    //    AuthEntity.ExpiresOn = jwtSecurityToken.ValidTo;
    //    AuthEntity.Roles = rolesList.ToList();

    //    return AuthEntity;
    //}

    //public async Task<string> AddRoleAsync(AddRoleModel model)
    //{
    //    var user = await _userManager.FindByIdAsync(model.UserId);

    //    if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
    //        return "Invalid user ID or Role";

    //    if (await _userManager.IsInRoleAsync(user, model.Role))
    //        return "User already assigned to this role";

    //    var result = await _userManager.AddToRoleAsync(user, model.Role);

    //    return result.Succeeded ? string.Empty : "Sonething went wrong";
    //}
    private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
    {
        try
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);

            List<Claim> roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            IEnumerable<Claim> claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            throw;
        }
    }
}
