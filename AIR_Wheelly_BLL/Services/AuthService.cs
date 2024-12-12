using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AIR_Wheelly_BLL.Helpers;
using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AIR_Wheelly_BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, JwtHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _jwtHelper = jwtHelper;
        }

        public async Task<User> RegisterUser(RegisterUserDTO dto)
        {
            bool exists = await _unitOfWork.UserRepository.GetUserByEmailAsync(dto.Email) != null;

            if (exists)
                throw new ArgumentException("Email is already taken!");

            User user = new()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
            user.Id = Guid.NewGuid();
            user.Password = PasswordHelper.HashPassword(user, dto.Password);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            user.Password = null;

            return user;
        }

        public async Task<string?> LoginUser(LoginUserDto dto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(dto.Email);
            if (user != null && PasswordHelper.VerifyPassword(user, user.Password, dto.Password))
            {
                return _jwtHelper.GenerateJwtToken(user.Id);
            }
            return null;
        }


        public async Task<User?> GetUserByJwt(string jwtToken)
        {
            var userId = _jwtHelper.GetUserIdFromJwt(jwtToken);
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            user.Password = null;
            return user;
        }

        public async Task<string?> OAuthLogin(string token)
        {
            var oauthUser = await OAuthHelper.ValidateToken(token);
            if (oauthUser is null)
                return null;

            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(oauthUser.email);

            if (user == null)
            {
                RegisterUserDTO dto = new()
                {
                    FirstName = oauthUser.given_name,
                    LastName = oauthUser.family_name,
                    Email = oauthUser.email,
                    Password = Guid.NewGuid().ToString()
                };
                try
                {
                    user = await RegisterUser(dto);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return _jwtHelper.GenerateJwtToken(user.Id);

        }

        public async Task<User?> UpdateProfileAsync(UpdateProfileDTO dto, string jwtToken)
        {
            var userId = _jwtHelper.GetUserIdFromJwt(jwtToken);
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user == null )
            {
                throw new KeyNotFoundException();
            }

            if (!string.IsNullOrEmpty(dto.FirstName))
            {
                user.FirstName = dto.FirstName;
            }

            if (!string.IsNullOrEmpty(dto.LastName))
            {
                user.LastName = dto.LastName;
            }

            if (!string.IsNullOrEmpty(dto.Email))
            {
                user.Email = dto.Email;
            }

            if (!string.IsNullOrEmpty(dto.NewPassword))
            {
                if (string.IsNullOrEmpty(dto.CurrentPassword) || !PasswordHelper.VerifyPassword(user, user.Password,dto.CurrentPassword))
                {
                    throw new UnauthorizedAccessException();
                }
                user.Password = PasswordHelper.HashPassword(user, dto.NewPassword);
            }

            await _unitOfWork.UserRepository.UpdateUserAsync(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }

    }
}
