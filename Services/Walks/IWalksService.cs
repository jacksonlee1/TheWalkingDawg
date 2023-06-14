using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Walks;

namespace Services.Walks
{
    public interface IWalksService
    {
        Task<bool> NewWalkAsync(CreateWalks model);
        Task<IEnumerable<WalksDetail>> GetWalkByDogIdAsync(int DogId);
        Task<bool> DeleteWalkByIdAsync(int Id);
        Task<bool> UpdateWalkAsync(int Id);

    }
}