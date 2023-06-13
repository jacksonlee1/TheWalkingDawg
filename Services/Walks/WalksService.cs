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
            return await _db.Walking.Where(c => c.DogId == id).Select(c => new WalksDetail
            {
                DogId = c.DogId,
                DistanceWalked = c.DistanceWalked,
                Lattitude = c.Lattitude,
                Longitude = c.Longitude,
                WalkerName = c.WalkerName,
                OutsideTemp = c.OutsideTemp,
                WalkStarted = c.WalkStarted,
                WalkEnded = c.WalkEnded

            }).ToListAsync();
        }


    }
}