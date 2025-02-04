﻿using AIR_Wheelly_Common.Interfaces.Repository;
using AIR_Wheelly_DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context) {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync() {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id) {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity) {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity) {
            _dbSet.Update(entity);
        }

        public void Delete(T entity) {
            _dbSet.Remove(entity);
        }
    }
}
