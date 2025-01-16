using AIR_Wheelly_Common.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_Common.Interfaces.Service
{
    public interface IStatisticService
    {
        Task<IEnumerable<GetNumberOfRentsPerCarResponseItemDTO>> GetNumberOfRentsPerCar(Guid userId);
    }
}
