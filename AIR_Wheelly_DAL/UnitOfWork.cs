using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_DAL.Data;
using AIR_Wheelly_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_DAL {
    public class UnitOfWork : IUnitOfWork, IDisposable {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private bool _disposed = false;

        public UnitOfWork(ApplicationDbContext context, IServiceProvider serviceProvider) {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IUserRepository UserRepository => _serviceProvider.GetService<IUserRepository>();

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
