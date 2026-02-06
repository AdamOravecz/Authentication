using Authentication.Models.Dtos;

namespace Authentication.Services.IAuthService
{
    public interface IAuth
    {
        Task<object> Register(RegisterRequestDto registerRequestDto);
        Task<object> AssingRole(string UserName,string RoleName);
        Task<object> Login(LoginRequestDto loginrequestdto);
    }
}
