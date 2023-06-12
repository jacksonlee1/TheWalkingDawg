using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Models.Rating;

namespace Services.Rating
{
    public interface IRatingService
    {
        Task<bool> NewRatingAsync(CreateRating model);
        Task<RatingEntity> GetRatingByIdAsync(int id);

        Task<bool> DeleteRatingByIdAsync(int id);
        Task<bool> UpdateRatingAsync(UpdateRating model);


    }
}