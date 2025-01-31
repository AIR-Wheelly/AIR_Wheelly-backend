using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AIR_Wheelly_Common.Models
{
    public class Review
    {
        public int Grade { get; set; }
        public Guid UserId { get; set; }
        public Guid CarListingId { get; set; }

        public User User { get; set; } = null!;
        [JsonIgnore]
        public CarListing CarListing { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
