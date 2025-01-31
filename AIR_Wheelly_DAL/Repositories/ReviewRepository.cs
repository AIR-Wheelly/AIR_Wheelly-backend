using AIR_Wheelly_Common.Interfaces.Repository;
using AIR_Wheelly_Common.Models;
using AIR_Wheelly_DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_DAL.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context) {}
    }
}
