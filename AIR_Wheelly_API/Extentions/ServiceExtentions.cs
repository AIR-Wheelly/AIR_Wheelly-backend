using AIR_Wheelly_BLL.Helpers;
using AIR_Wheelly_BLL.Services;
using AIR_Wheelly_Common.Interfaces.Repository;
using AIR_Wheelly_Common.Interfaces.Service;
using AIR_Wheelly_DAL.Repositories;

namespace AIR_Wheelly_API.Extentions
{
    public static class ServiceExtentions {
        public static void AddRepositories(this IServiceCollection services) {
            services.AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<ILocationRepository, LocationRepository>()
                    .AddScoped<ICarListingRepository, CarListingRepository>()
                    .AddScoped<ICarListingPicturesRepository, CarListingPicturesRepository>()
                    .AddScoped<IManafacturerRepository, ManafacturerRepository>();
        }

        public static void AddServices(this IServiceCollection services) {
            services.AddScoped<IAuthService, AuthService>()
                    .AddScoped<ICarService, CarService>()
                    .AddScoped<ILocationService, LocationService>();
        }

        public static void AddHelpers(this IServiceCollection services) {
            services.AddSingleton<JwtHelper>();
        }
    }
}
