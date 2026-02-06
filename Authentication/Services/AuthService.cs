using Authentication.Services.IAuthService;
using Authentication.Models.Dtos;
using Authentication.Datas;
using Microsoft.AspNetCore.Identity;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Services
{
    public class AuthService : IAuth
    {
		private readonly AppDbContext _context;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
        public ResponseDto responseDto = new();

        public AuthService(AppDbContext context,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,ResponseDto responseDto)
		{
			_context = context;
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.responseDto = new ResponseDto();
        }

        public async Task<object> AssingRole(string UserName, string RoleName)
        {
			try
			{
				var user = await _context.ApplicationUsers.FirstOrDefaultAsync(user => user.NormalizedUserName == UserName.ToUpper());
				
				if(user != null)
				{
					if (!roleManager.RoleExistsAsync(RoleName).GetAwaiter().GetResult())
					{
						roleManager.CreateAsync(new IdentityRole(RoleName)).GetAwaiter().GetResult();
                    }
					await userManager.AddToRoleAsync(user, RoleName);

                    responseDto.Message = "Sikeres hozzárendelés";
                    responseDto.Result = user;
                    return responseDto;
                }
                responseDto.Message = "Sikertelen hozzárendelés";
                responseDto.Result = null;
                return responseDto;
            }
			catch (Exception ex)
			{
                responseDto.Message = ex.Message;
                responseDto.Result = ex.HResult;
                return responseDto;
            }
        }

        public async Task<object> Login(LoginRequestDto loginrequestdto)
        {
            throw new NotImplementedException();
        }

        public async Task<object> Register(RegisterRequestDto registerRequestDto)
        {
			try
			{
				var user = new ApplicationUser
				{
					UserName = registerRequestDto.UserName,
					Email = registerRequestDto.Email,
					FullName = registerRequestDto.FullName
				};
				var result = await userManager.CreateAsync(user, registerRequestDto.Password);

				if (result.Succeeded)
				{
					var userReturn = await _context.ApplicationUsers.FirstOrDefaultAsync(user => user.UserName == registerRequestDto.UserName);

					responseDto.Message ="Sikeres regisztrácio";
					responseDto.Result = userReturn;
					return responseDto;
                }
				responseDto.Message = result.Errors.FirstOrDefault().Description;
                responseDto.Result = null;
                return responseDto;
            }
			catch (Exception ex)
			{
                responseDto.Message = ex.Message;
                responseDto.Result = ex.HResult;
                return responseDto;
            }
        }
    }
}
