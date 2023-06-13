

using Data.Entities;
using Models.Dogs;

namespace Services.DogServices;

public interface IDogService
{
    Task<bool> CreateDogAsync(DogCreate model);

    Task<IEnumerable<DogDetail>> GetAllDogsAsync();

    Task<DogsEntity> GetDogByIdAsync(int id);

    Task<bool> DeleteDogByIdAsync(int id);
}