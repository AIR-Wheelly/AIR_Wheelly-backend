using AIR_Wheelly_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_Common.DTO.Response
{
    public class GetNumberOfRentsPerCarResponseItemDTO
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public CarListing Car { get; set; }
    }
}
