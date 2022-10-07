using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_JWT.Controllers
{
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
}
