

using Models.Dogs;

namespace Services.DogServices;

public interface IDogService
{
    Task<bool> CreateDogAsync(DogCreate model);

    Task<IEnumerable<DogDetail>> GetAllDogsAsync(int id);
}