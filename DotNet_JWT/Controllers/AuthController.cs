namespace DotNet_JWT;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("Register")] public async Task<IActionResult> Register([FromBody] RegisterEntity register)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        AuthEntity result = await _authService.RegisterAsync(register);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost("Login")] public async Task<IActionResult> LogIn([FromBody] LogInModel logInModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        AuthEntity result = await _authService.LogInAsync(logInModel);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost("AddRole")]
    public async Task<IActionResult> AddRole([FromBody] RoleModel roleModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string result = await _authService.AddRoleAsync(roleModel);

        if (!string.IsNullOrEmpty(result))
            return BadRequest(result);

        return Ok(result);
    }
}
