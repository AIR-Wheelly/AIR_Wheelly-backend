using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_DAL.Models;
using Microsoft.IdentityModel.Tokens;

namespace AIR_Wheelly_BLL.Services {
    public class AuthService {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> RegisterUser(RegisterUserDTO dto) {
            User user = new() {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password//HashPassword(dto.Password)
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            user.Password = null;

            return user;
        }

        public async Task<User?> LoginUser(LoginUserDto dto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(dto.Email);
            if (user != null && user.Password == dto.Password)
            {
                user.Password = null;
                return user;
            }

            return null;
        }

        public string GenerateJwtToken(int Id)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("f0085aed6406bfd10a953bdbd2e8b2c706262beae0dd7ba939e7d557e43f70ad88fd664130779f209491440a95ee75420b44ae566fb320a25bda283eb9268b2eeac32d34095d3417b108a8f7539c5e458648daa24e38061060972c8456127b27deaef1db5b8bdbb6ef4d59dd076daccf66c1a42edd0ed0037ddc3442efec9df822b72ae8f72cb225a81365d2795c7e4a8cb0dd5d306e6fb93b573b8a9264d5a191c9135c333720104447b0481be28a30443fbb51f6a9d6f2f20178b505ea3ce6df5b916730ab3673a9929d56c67ff6c76c358032d7adfcace8f5adab0117643a5d4cbcc8d6574d4f15a51c1d56223bd4ac41ecd66a51aff52cfe63741ebcd88f");
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
    }
}
