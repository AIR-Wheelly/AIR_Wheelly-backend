using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Interfaces.Repository;
using AIR_Wheelly_Common.Models;
using AIR_Wheelly_DAL.Data;
using AIR_Wheelly_DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace AIR_Wheelly_DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private bool _disposed = false;

        public UnitOfWork(ApplicationDbContext context, IServiceProvider serviceProvider) {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IUserRepository UserRepository => _serviceProvider.GetService<IUserRepository>();
        public ILocationRepository LocationRepository => _serviceProvider.GetService<ILocationRepository>();
        public ICarListingRepository CarListingRepository => _serviceProvider.GetService<ICarListingRepository>();
        public ICarListingPicturesRepository CarListingPicturesRepository => _serviceProvider.GetService<ICarListingPicturesRepository>();
        public IManafacturerRepository ManafacturerRepository => _serviceProvider.GetService<IManafacturerRepository>();
        public IModelRepository ModelRepository => _serviceProvider.GetService<IModelRepository>();
        public  ICarReservationRepository CarReservationRepository => _serviceProvider.GetService<ICarReservationRepository>();
        public IReviewRepository ReviewRepository => _serviceProvider.GetService<IReviewRepository>();

        public async Task<int> CompleteAsync() {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
