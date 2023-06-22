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
using Models.User;
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
                OwnerId=_userId,
                WalkId = model.WalkId,
                Score = model.Score,
                Comment = model.Comment,
                WalkerId = model.WalkerId
            };
            _db.Ratings.Add(entity);
            var numChanges =  await _db.SaveChangesAsync();
            return numChanges == 1;

        }
        public async Task<RatingEntity?> GetRatingByIdAsync(int id)
        {
           return await _db.Ratings.Include(r=>r.Walk).FirstOrDefaultAsync(r=>r.Id == id);


        }

        public async Task<RatingEntity?> GetRatingByCurrentUserAsync()
        {
           return await _db.Ratings.Include(r=>r.Walk).FirstOrDefaultAsync(r=>r.OwnerId == _userId);


        }
          public async Task<RatingEntity?> GetRatingByCurrentWalkerAsync()
        {
           return await _db.Ratings.Include(r=>r.Walk).FirstOrDefaultAsync(r=>r.OwnerId == _userId);


        }
        public async Task<List<RatingDetail>> GetRatingsAsync()
        {
            return await _db.Ratings.Include(r=>r.Walker).Include(r=>r.Owner).Select(r => 
            new RatingDetail
            {
                Id = r.Id,
                Username = r.Owner.Username,
                WalkerName = r.Walker.Username, 
                WalkId = r.WalkId,
                Score = r.Score,
                Comment = r.Comment
            }).ToListAsync();
        }

        public async Task<bool> DeleteRatingByIdAsync(int id)
        {
            var entity = await _db.Ratings.FindAsync(id);
            if(entity is null) return false;
            _db.Ratings.Remove(entity);
            var numChanges = await  _db.SaveChangesAsync();
            return numChanges == 1;

        }


        public async Task<bool> UpdateRatingAsync(UpdateRating model)
        {
            var entity = await _db.Ratings.FindAsync(model.Id);
            if(entity is null) return false;
            entity.Score = model.Score;
            entity.Comment = model.Comment;

            var numChanges = await  _db.SaveChangesAsync();
            return numChanges == 1;

        }

        public async Task<IEnumerable<RatingDetail>?> GetRatingsByUserId(int id)
        {


            return  await _db.Ratings.Include(r=>r.Owner).Include(r=>r.Walker).Where(r => r.OwnerId == id).Select(r => new RatingDetail
            {
                Id=r.Id,
                WalkId = r.WalkId,
                Score = r.Score,
                Comment = r.Comment,
                Username = r.Owner.Name,
                WalkerName = r.Walker.Name

                
            }).ToListAsync();

            
        }




          public async Task<IEnumerable<RatingDetail>?> GetRatingsByWalkId(int id)
        {
            return  await _db.Ratings.Include(r=>r.Owner).Include(r=>r.Walker).Where(r => r.WalkId == id).Select(r => new RatingDetail
            {
            
                WalkId = r.WalkId,
                Score = r.Score,
                Comment = r.Comment,
                Username= r.Owner.Name,
                WalkerName = r.Walker.Name
           
            }).ToListAsync();

            
        }

    

      

    }
}