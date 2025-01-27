using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_Common.DTO.Response
{
    public class CreateReviewDTO
    {
        [Required]
        [Range(1,5)]
        public int Grade { get; set; }
        [Required]
        public Guid ListingId { get; set; }
    }
}
