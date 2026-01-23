using Authentication.Models.Dtos;
using Authentication.Services.IAuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth auth;

        public AuthController(IAuth auth)
        {
            this.auth = auth;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            var user = await auth.Register(registerRequestDto);
            if(user != null)
            {
                return StatusCode(201,user);
            }
            return BadRequest("User registration failed");
        }
        [HttpPost("AssingRole")]
        public async Task<IActionResult> AssingRole(string UserName, string RoleName)
        {
            var user = await auth.AssingRole(UserName, RoleName);
            if (user != null)
            {
                return StatusCode(201, user);
            }
            return BadRequest("Role assignment failed");
        }

    }
}
