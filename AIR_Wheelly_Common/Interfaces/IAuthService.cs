using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Models;

namespace AIR_Wheelly_Common.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterUser(RegisterUserDTO dto);
        Task<User?> LoginUser(LoginUserDto dto);
        string GenerateJwtToken(int Id);
        Task<User?> GetUserByJwt(string jwtToken);
        Task<User?> OAuthLogin(string token);
        Task<User?> UpdateProfileAsync(UpdateProfileDTO dto, string jwtToken);
    }
}
