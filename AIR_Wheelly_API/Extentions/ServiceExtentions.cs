using AIR_Wheelly_BLL.Helpers;
using AIR_Wheelly_BLL.Services;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_DAL.Repositories;

namespace AIR_Wheelly_API.Extentions {
    public static class ServiceExtentions {
        public static void AddRepositories(this IServiceCollection services) {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void AddServices(this IServiceCollection services) {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<FuelTypeService>();
        }

        public static void AddHelpers(this IServiceCollection services) {
            services.AddScoped<IPasswordHelper, PasswordHelper>();
        }
    }
}
