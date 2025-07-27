using AuthAPI.Models.Dto;

namespace AuthAPI.Service.Abstraction
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        //Task<bool> AssignRole(string email, string roleName);
    }
}
