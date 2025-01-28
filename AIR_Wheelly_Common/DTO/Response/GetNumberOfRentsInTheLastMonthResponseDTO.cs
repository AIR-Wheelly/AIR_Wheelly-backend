using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_Common.DTO.Response
{
    public class GetNumberOfRentsInTheLastMonthResponseDTO
    {
        public int Count { get; set; }
        public GetNumberOfRentsPerCarResponseItemDTO Listing { get; set; }
    }
}
