using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Models.Walks;

namespace Services.Walks
{
    public interface IWalksService
    {
        Task<bool> NewWalkAsync(CreateWalks model);
        Task<IEnumerable<WalksDetail>> GetWalkByDogIdAsync(int DogId);
        Task<bool> EndWalkByIdAsync(int id);
        Task<bool> StartWalkByIdAsync(int id);
        Task<IEnumerable<WalksDetail>> GetWalksByCurrentIdAsync();
        Task<IEnumerable<WalksDetail>> GetOngoingWalksByCurrentIdAsync();
        Task<IEnumerable<WalksDetail>> GetFinishedWalksByCurrentIdAsync();
        Task<IEnumerable<WalksDetail>> GetAvailableWalksByCurrentIdAsync();
        Task<IEnumerable<WalkingEntity?>?> GetAllWalksAsync();
        

        Task<bool> DeleteWalkByIdAsync(int Id);
        Task<bool> UpdateWalkAsync(WalksUpdate req);
        Task<bool> FinishWalkAsync(FinishWalk req);

      Task<WalkingEntity?> GetWalkByIdAsync(int id);
      
    }
}