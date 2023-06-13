using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Models.Rating;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
// dotnet add Services package Microsoft.AspNetCore.Http;
namespace Services.Rating
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _db;

        private readonly int _userId;

        public RatingService(IHttpContextAccessor httpContext,ApplicationDbContext db)
        {
            _db = db;
            var userClaims = httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims?.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _userId);
            if (!validId)
            {
                throw new Exception("Attempted to build RatingService without User Id Claim");
            }
        }
        public async Task<bool> NewRatingAsync(CreateRating model)
        {
            var entity = new RatingEntity(){
                OwnerId = _userId,
                WalkId = model.WalkId,
                Score = model.Score,
                Comment = model.Comment
            };
            _db.Ratings.Add(entity);
            var numChanges =  await _db.SaveChangesAsync();
            return numChanges == 1;

        }
        public async Task<RatingEntity> GetRatingByIdAsync(int id)
        {
           return await _db.Ratings.Include(r=>r.Owner).FirstOrDefaultAsync(r=>r.Id == id);


        }
        public async Task<List<RatingDetail>> GetRatingsAsync()
        {
            return await _db.Ratings.Include(r=>r.Owner).Include(r=>r.Walker).Select(r => new RatingDetail
            {
                Username = r.Owner.Username,
                WalkId = r.WalkId,
                Score = r.Score,
                Comment = r.Comment
            }).ToListAsync();
        }

        public async Task<bool> DeleteRatingByIdAsync(int id)
        {
            var entity = await _db.Ratings.FindAsync(id);
            _db.Ratings.Remove(entity);
            var numChanges = await  _db.SaveChangesAsync();
            return numChanges == 1;

        }
        public async Task<bool> UpdateRatingAsync(UpdateRating model)
        {
            var entity = await _db.Ratings.FindAsync(model.Id);
            entity.Score = model.Score;
            entity.Comment = model.Comment;

            var numChanges = await  _db.SaveChangesAsync();
            return numChanges == 1;

        }

        public async Task<IEnumerable<RatingDetail>?> GetRatingsByUserId(int id)
        {
            return  await _db.Ratings.Include(r=>r.Owner).Where(r => r.OwnerId == id).Select(r => new RatingDetail
            {
                WalkId = r.WalkId,
                Score = r.Score,
                Comment = r.Comment,
                Username = r.Owner.Username
            }).ToListAsync();

            
        }


          public async Task<IEnumerable<RatingDetail>?> GetRatingsByWalkId(int id)
        {
            return  await _db.Ratings.Include(r=>r.Owner).Where(r => r.WalkId == id).Select(r => new RatingDetail
            {
                WalkId = r.WalkId,
                Score = r.Score,
                Comment = r.Comment,
                Username = r.Owner.Username
            }).ToListAsync();

            
        }


    }
}