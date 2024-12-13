using AIR_Wheelly_Common.DTO;
using AIR_Wheelly_Common.Interfaces.Repository;
using AIR_Wheelly_Common.Models;
using AIR_Wheelly_DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_DAL.Repositories
{
    public class ModelRepository : Repository<Model>, IModelRepository
    {
        public ModelRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Model>> GetModelsByManafacturerIdAsync(Guid id)
        {
            return await _dbSet.Where(m => m.ManafacturerId == id).ToListAsync();
        }
    }
}
