using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_BLL.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _work;

        public StatisticService(IUnitOfWork work)
        {
            _work = work;
        }


    }

}
