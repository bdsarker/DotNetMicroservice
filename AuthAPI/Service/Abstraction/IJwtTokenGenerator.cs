using AuthAPI.Models;

namespace AuthAPI.Service.Abstraction
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
