using Authentication.Models;

namespace Authentication.Services.IAuthService
{
    public interface ITokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser,IEnumerable<string> role);
    }
}
