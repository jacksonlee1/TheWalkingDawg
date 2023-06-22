using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Walks;

namespace Services.Walks
{
    public class WalksService : IWalksService
    {
        private readonly ApplicationDbContext _db;
        private readonly int _userId;

        public WalksService(IHttpContextAccessor httpContext, ApplicationDbContext db)
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

        public async Task<bool> NewWalkAsync(CreateWalks model)
        {
            var entity = new WalkingEntity
            {
                DistanceWalked = model.DistanceWalked,
                Lat = model.Lattitude,
                Long = model.Longitude,
                WalkerId = model.WalkerId,
                OutsideTemp = model.OutsideTemp,

            };
            _db.Walks.Add(entity);
            var changes = await _db.SaveChangesAsync();
            return changes == 1;

        }


         public async Task<WalkingEntity?> GetWalkByIdAsync(int id)
        {
            return await _db.Walks.Include(w => w.Dog).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<WalksDetail>> GetWalkByDogIdAsync(int id)
        {
            return await _db.Walks.Include(w => w.Dog).Where(c => c.DogId == id).Select(c => new WalksDetail
            {
                WalkId = c.Id,
                DogId = c.DogId,
                DistanceWalked = c.DistanceWalked,
                Lattitude = c.Lat,
                Longitude = c.Long,
                WalkerId = c.WalkerId,
                OutsideTemp = c.OutsideTemp,
                WalkStarted = c.WalkStarted,
                WalkEnded = c.WalkEnded,
                DogName = c.Dog.Name

            }).ToListAsync();
        }

        public async Task<bool> UpdateWalkAsync(WalksUpdate req)
        {
            var entity = await _db.Walks.FindAsync(req.Id);
            if (entity is null) return false;
            entity.DogId = req.DogId;
            entity.DistanceWalked = req.DistanceWalked;
            entity.Lat = req.Lattitude;
            entity.Long = req.Longitude;
            entity.WalkerId = req.WalkerId;
            entity.OutsideTemp = req.OutsideTemp;
            entity.WalkStarted = req.WalkStarted;
            entity.WalkEnded = req.WalkEnded;
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }

        public async Task<bool> DeleteWalkByIdAsync(int Id)
        {
            var walks = await _db.Walks.FindAsync(Id);
            if (walks is null) return false;
            _db.Walks.Remove(walks);
            var changed = await _db.SaveChangesAsync();
            return changed == 1;
        }


        public async Task<IEnumerable<WalksDetail>> GetWalksByCurrentIdAsync()
        {
        
            return await _db.Walks.Include(w => w.Dog).Where(c => c.WalkerId == _userId).Select(c => new WalksDetail
            {

                WalkId = c.Id,
                DogId = c.DogId,
                DistanceWalked = c.DistanceWalked,
                Lattitude = c.Lat,
                Longitude = c.Long,
                WalkerId = c.WalkerId,
                OutsideTemp = c.OutsideTemp,
                WalkStarted = c.WalkStarted,
                WalkEnded = c.WalkEnded,
                DogName = c.Dog.Name

            }).ToListAsync();
        }


         public async Task<IEnumerable<WalksDetail>> GetAvailableWalksByCurrentIdAsync()
        {
         
            return await _db.Walks.Include(w => w.Dog).Where(c => c.WalkerId == _userId&& c.WalkStarted == DateTime.UnixEpoch).Select(c => new WalksDetail
            {

                WalkId = c.Id,
                DogId = c.DogId,
                DistanceWalked = c.DistanceWalked,
                Lattitude = c.Lat,
                Longitude = c.Long,
                WalkerId = c.WalkerId,
                OutsideTemp = c.OutsideTemp,
                WalkStarted = c.WalkStarted,
                WalkEnded = c.WalkEnded,
                DogName = c.Dog.Name

            }).ToListAsync();
        }
        public async Task<IEnumerable<WalksDetail>> GetOngoingWalksByCurrentIdAsync()
        {
            return await _db.Walks.Include(w => w.Dog).Where(c => c.WalkerId == _userId).Where(c => c.WalkStarted != DateTime.UnixEpoch && c.WalkEnded == DateTime.UnixEpoch).Select(c => new WalksDetail
            {
                WalkId = c.Id,
                DogId = c.DogId,
                DistanceWalked = c.DistanceWalked,
                Lattitude = c.Lat,
                Longitude = c.Long,
                WalkerId = c.WalkerId,
                OutsideTemp = c.OutsideTemp,
                WalkStarted = c.WalkStarted,
                WalkEnded = c.WalkEnded,
                DogName = c.Dog.Name

            }).ToListAsync();
        }

 public async Task<IEnumerable<WalksDetail>> GetFinishedWalksByCurrentIdAsync()
        {
            return await _db.Walks.Include(w => w.Dog).Where(c => c.WalkerId == _userId).Where(c => c.WalkEnded != DateTime.UnixEpoch).Select(c => new WalksDetail
            {
                WalkId = c.Id,
                DogId = c.DogId,
                DistanceWalked = c.DistanceWalked,
                Lattitude = c.Lat,
                Longitude = c.Long,
                WalkerId = c.WalkerId,
                OutsideTemp = c.OutsideTemp,
                WalkStarted = c.WalkStarted,
                WalkEnded = c.WalkEnded,
                DogName = c.Dog.Name

            }).ToListAsync();
        }



        public async Task<bool> EndWalkByIdAsync(int id)
        {
            var entity = await _db.Walks.FindAsync(id);
            if (entity is null) return false;
            entity.WalkEnded = DateTime.Now;
            return await _db.SaveChangesAsync() == 1;
        }
        public async Task<bool> StartWalkByIdAsync(int id)
        {
            var entity = await _db.Walks.FindAsync(id);
            if (entity is null) return false;
            entity.WalkStarted = DateTime.Now;
            entity.DistanceWalked = 0;
            return await _db.SaveChangesAsync() == 1;
        }


        public async Task<bool> FinishWalkAsync(FinishWalk pos)
        {
            var entity = await _db.Walks.FindAsync(pos.Id);
            if (entity is null) return false;
            entity.Id = pos.Id;
            
            entity.DistanceWalked = pos.DistanceWalked;
            entity.Lat = pos.Lattitude;
            entity.Long = pos.Longitude;
            entity.OutsideTemp = pos.OutsideTemp;
            entity.WalkEnded =DateTime.Now;
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }

      

    }
}