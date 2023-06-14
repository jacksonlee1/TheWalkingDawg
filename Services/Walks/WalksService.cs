using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Walks;

namespace Services.Walks
{
    public class WalksService : IWalksService
    {
        private readonly ApplicationDbContext _db;

        public WalksService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> NewWalkAsync(CreateWalks model)
        {
            var entity = new WalkingEntity
            {
                DogId = model.DogId,
                DistanceWalked = model.DistanceWalked,
                Lattitude = model.Lattitude,
                Longitude = model.Longitude,
                WalkerName = model.WalkerName,
                OutsideTemp = model.OutsideTemp,
                WalkStarted = model.WalkStarted,
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
                WalkerName = c.WalkerName,
                OutsideTemp = c.OutsideTemp,
                WalkStarted = c.WalkStarted,
                WalkEnded = c.WalkEnded,
                DogName = c.Dog.Name

            }).ToListAsync();
        }

        public async Task<bool> UpdateWalkAsync(WalksUpdate req)
            {
                var entity = await _db.Walking.FindAsync(req.Id);
                entity.Id = req.Id;
                entity.DogId = req.DogId;
                entity.DistanceWalked = req.DistanceWalked;
                entity.Lattitude = req.Lattitude;
                entity.Longitude = req.Longitude;
                entity.WalkerName = req.WalkerName;
                entity.OutsideTemp = req.OutsideTemp;
                entity.WalkStarted = req.WalkStarted;
                entity.WalkEnded = req.WalkEnded;
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }

        public async Task<bool> DeleteWalkByIdAsync(int Id)
        {
            var walks = await _db.Walking.FindAsync(Id);
            _db.Walking.Remove(walks);
            var changed = await _db.SaveChangesAsync();
            return changed == 1;
        }
    }
}