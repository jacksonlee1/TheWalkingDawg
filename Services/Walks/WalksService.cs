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

        public WalksService(IHttpContextAccessor httpContext,ApplicationDbContext db)
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
                DogId = model.DogId,
                DistanceWalked = model.DistanceWalked,
                Lattitude = model.Lattitude,
                Longitude = model.Longitude,
                WalkerId = model.WalkerId,
                OutsideTemp = model.OutsideTemp,
                WalkStarted = DateTime.UnixEpoch,
                WalkEnded = model.WalkEnded
            };
            _db.Walking.Add(entity);
            var changes = await _db.SaveChangesAsync();
            return changes == 1;

        }

        public async Task<IEnumerable<WalksDetail>> GetWalkByDogIdAsync(int id)
        {
            return await _db.Walking.Include(w => w.Dog).Where(c => c.DogId == id).Select(c => new WalksDetail
            {
                DogId = c.DogId,
                DistanceWalked = c.DistanceWalked,
                Lattitude = c.Lattitude,
                Longitude = c.Longitude,
                WalkerId = c.WalkerId,
                OutsideTemp = c.OutsideTemp,
                WalkStarted = c.WalkStarted,
                WalkEnded = c.WalkEnded,
                DogName = c.Dog.Name

            }).ToListAsync();
        }

        public async Task<bool> UpdateWalkAsync(WalksUpdate req)
            {
                var entity = await _db.Walking.FindAsync(req.Id);
                if(entity is null) return false;
                entity.DogId = req.DogId;
                entity.DistanceWalked = req.DistanceWalked;
                entity.Lattitude = req.Lattitude;
                entity.Longitude = req.Longitude;
                entity.WalkerId = req.WalkerId;
                entity.OutsideTemp = req.OutsideTemp;
                entity.WalkStarted = req.WalkStarted;
                entity.WalkEnded = req.WalkEnded;
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }

        public async Task<bool> DeleteWalkByIdAsync(int Id)
        {
            var walks = await _db.Walking.FindAsync(Id);
            if(walks is null) return false;
            _db.Walking.Remove(walks);
            var changed = await _db.SaveChangesAsync();
            return changed == 1;
        }


        public async Task<IEnumerable<WalksDetail>> GetWalksByCurrentIdAsync()
        {
            return await _db.Walking.Include(w => w.Dog).Where(c => c.WalkerId == _userId).Where(c=>c.WalkStarted == DateTime.UnixEpoch).Select(c => new WalksDetail
            {
                DogId = c.DogId,
                DistanceWalked = c.DistanceWalked,
                Lattitude = c.Lattitude,
                Longitude = c.Longitude,
                WalkerId = c.WalkerId,
                OutsideTemp = c.OutsideTemp,
                WalkStarted = c.WalkStarted,
                WalkEnded = c.WalkEnded,
                DogName = c.Dog.Name

            }).ToListAsync();
        }

        public async Task<IEnumerable<WalksDetail>> GetOngoingWalksByCurrentIdAsync()
        {
            return await _db.Walking.Include(w => w.Dog).Where(c => c.WalkerId == _userId).Where(c=>c.WalkStarted != DateTime.UnixEpoch && c.WalkEnded == null).Select(c => new WalksDetail
            {
                DogId = c.DogId,
                DistanceWalked = c.DistanceWalked,
                Lattitude = c.Lattitude,
                Longitude = c.Longitude,
                WalkerId = c.WalkerId,
                OutsideTemp = c.OutsideTemp,
                WalkStarted = c.WalkStarted,
                WalkEnded = c.WalkEnded,
                DogName = c.Dog.Name

            }).ToListAsync();
        }


        
public async Task<bool> EndWalkByIdAsync(int id)
        {
            var entity = await _db.Walking.FindAsync(id);
                if(entity is null) return false;
            entity.WalkEnded = DateTime.Now;
             return await _db.SaveChangesAsync() == 1;
        } 
        public async Task<bool> StartWalkByIdAsync(int id)
        {
            var entity = await _db.Walking.FindAsync(id);
                if(entity is null) return false;
            entity.WalkStarted = DateTime.Now;
            entity.DistanceWalked =0;
             return await _db.SaveChangesAsync() == 1;
        } 
        

        public async Task<bool> FinishWalkByIdAsync(FinishWalk pos)
        {
                var entity = await _db.Walking.FindAsync(pos.Id);
                entity.Id = pos.Id;
                entity.DogId = pos.DogId;
                entity.DistanceWalked = pos.DistanceWalked;
                entity.Lattitude = pos.Lattitude;
                entity.Longitude = pos.Longitude;
                entity.WalkerId = pos.WalkerId;
                entity.OutsideTemp = pos.OutsideTemp;
                entity.WalkStarted = pos.WalkStarted;
                entity.WalkEnded = pos.WalkEnded;
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }

        public Task<bool> UpdateWalkAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FinishWalkByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}