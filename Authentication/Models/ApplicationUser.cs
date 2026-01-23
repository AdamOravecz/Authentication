using Microsoft.AspNetCore.Identity;

namespace Authentication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
