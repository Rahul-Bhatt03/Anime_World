using AnimeWorld.Data;
using AnimeWorld.Interfaces;
using System.Security.Claims;

namespace AnimeWorld.Services
{
  public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor,
        ApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public int UserId 
    {
        get 
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User identifier is missing");
                
            if (!int.TryParse(userId, out var id))
                throw new UnauthorizedAccessException("Invalid user identifier format");
                
            var userExists = _context.Users.Any(u => u.Id == id);
            if (!userExists)
                throw new UnauthorizedAccessException("User not found in database");
                
            return id;
        }
    }
    
    public string Username 
    {
        get
        {
            var username = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            return username ?? throw new UnauthorizedAccessException("Username claim is missing");
        }
    }
}
}
