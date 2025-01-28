using AIR_Wheelly_Common.DTO.Response;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Interfaces.Service;
using AIR_Wheelly_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<GetNumberOfRentsInTheLastMonthResponseDTO> GetLastMonthsStatistics(Guid userId)
        {
            List<CarReservation> allUserCarReservations = await _work.CarReservationRepository.GetReservationForOwner(userId);

            DateTime lastMonth = DateTime.Now.AddMonths(-1);
            List<CarReservation> allUserCarReservationsInTheLastMonth = allUserCarReservations.Where(x => x.CreatedAt.Month == lastMonth.Month && x.CreatedAt.Year == lastMonth.Year).ToList();

            GetNumberOfRentsPerCarResponseItemDTO? mostRentedCar = allUserCarReservationsInTheLastMonth
               .Select(r => r.CarListing)
               .GroupBy(l => l.Id)
               .Select(g => new GetNumberOfRentsPerCarResponseItemDTO
               {
                   Id = g.Key,
                   Count = g.Count(),
                   Car = g.First()
               })
               .OrderByDescending(item => item.Count) 
               .FirstOrDefault(); 

            return new GetNumberOfRentsInTheLastMonthResponseDTO()
            {
                Count = allUserCarReservationsInTheLastMonth.Count,
                Listing = mostRentedCar
            };


        }

        public async Task<IEnumerable<GetNumberOfRentsPerCarResponseItemDTO>> GetNumberOfRentsPerCar(Guid userId)
        {
            List<CarReservation> allUserCarReservations = await _work.CarReservationRepository.GetReservationForOwner(userId);
            IEnumerable<GetNumberOfRentsPerCarResponseItemDTO> numberOfRentsPerCar = allUserCarReservations.Select(r => r.CarListing).GroupBy(l => l.Id).Select(g => new GetNumberOfRentsPerCarResponseItemDTO()
            {
                Id = g.Key,
                Count = g.Count(),
                Car = g.First()
            });
            return numberOfRentsPerCar;
        }
    }

}
