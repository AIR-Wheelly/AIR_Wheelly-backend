using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Models;
using AIR_Wheelly_DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_DAL.Repositories
{
    public class ManafacturerRepository : Repository<Manafacturer>, IManafacturerRepository
    {
        public ManafacturerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
