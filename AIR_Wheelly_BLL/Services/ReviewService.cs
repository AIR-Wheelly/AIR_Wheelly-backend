using AIR_Wheelly_Common.DTO.Response;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_Common.Interfaces.Repository;
using AIR_Wheelly_Common.Interfaces.Service;
using AIR_Wheelly_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _work;

        public ReviewService(IUnitOfWork work)
        {
            _work = work;
        }

        public async Task<Review> CreateReview(CreateReviewDTO dto, Guid userId)
        {
            var listing = await _work.CarListingRepository.GetByIdAsync(dto.ListingId);

            if (listing == null)
            {
                throw new ArgumentException($"Listing {dto.ListingId} does not exist");
            }

            var reservation = await _work.CarReservationRepository.GetByListingAndUserId(dto.ListingId, userId);
            if (reservation == null)
            {
                throw new ArgumentException($"User {userId} has no reservations for listing {dto.ListingId}");
            }

            var review = new Review()
            {
                CarListingId = dto.ListingId,
                UserId = userId,
                Grade = dto.Grade
            };

            await _work.ReviewRepository.AddAsync(review);
            await _work.CompleteAsync();

            return review;
        }
    }
}
