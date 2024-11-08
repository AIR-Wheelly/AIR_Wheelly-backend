using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_DAL.Models;

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
    }
}
