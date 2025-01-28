using AIR_Wheelly_Common.DTO.Response;
using AIR_Wheelly_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_Common.Interfaces.Service
{
    public interface IReviewService
    {
        Task<Review> CreateReview(CreateReviewDTO dto, Guid userId);
    }
}
