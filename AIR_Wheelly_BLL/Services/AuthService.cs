using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AIR_Wheelly_BLL.Services {
    public class AuthService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHelper _passwordHelper;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IPasswordHelper passwordHelper) {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _passwordHelper = passwordHelper;
        }

        public async Task<User> RegisterUser(RegisterUserDTO dto) {
            bool exists = await _unitOfWork.UserRepository.GetUserByEmailAsync(dto.Email) != null;

            if (exists) throw new ArgumentException("Email is already taken!");

            User user = new() {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            user.Password = _passwordHelper.HashPassword(user, dto.Password);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            user.Password = null;

            return user;
        }

        public async Task<User?> LoginUser(LoginUserDto dto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(dto.Email);
            if (user != null && _passwordHelper.VerifyPassword(user, user.Password, dto.Password))
            {
                user.Password = null;
                return user;
            }

            return null;
        }

        public string GenerateJwtToken(int Id)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", Id.ToString()) }),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);

        }
        public async Task<User?> GetUserByJwt(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(jwtToken) as JwtSecurityToken;
            var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == "id") ?? throw new ArgumentNullException("No id claim found");
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userIdClaim.Value);
            user.Password = null;
            return user;
        }
    }
}
