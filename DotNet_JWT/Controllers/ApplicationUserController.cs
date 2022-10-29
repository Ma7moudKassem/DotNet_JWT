using Microsoft.AspNetCore.Authorization;

namespace DotNet_JWT;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ApplicationUserController : ControllerBase
{
    private readonly DbSet<ApplicationUser> dbSet;
    private readonly MigrationDbContext _context;
    public ApplicationUserController(MigrationDbContext context)
    {
        _context = context;
        dbSet = _context.Set<ApplicationUser>();
    }
    [HttpPost]
    public async Task Post([FromForm] ApplicationUser user)
    {
        await dbSet.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
