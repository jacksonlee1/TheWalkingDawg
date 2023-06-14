

using Data.Entities;
using Models.Dogs;

namespace Services.DogServices;

public interface IDogService
{
    Task<bool> CreateDogAsync(DogCreate model);

    Task<IEnumerable<DogDetail>> GetAllDogsAsync();

    Task<DogsEntity> GetDogByIdAsync(int id);

    Task<IEnumerable<DogsEntity>> GetDogByOwnerIdAsync(int id);

    Task<IEnumerable<DogsEntity>> GetDogsByCurrentUserAsync();

    Task<bool> UpdateDogAsync(DogUpdate request);

    Task<bool> DeleteDogByIdAsync(int id);
}