
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Dogs;

namespace Services.DogServices;

public class DogService : IDogService
{
    private readonly ApplicationDbContext _context;

    public DogService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateDogAsync(DogCreate model)
    {
        var entity = new DogsEntity
        {
            OwnerID = model.OwnerID,
            Name = model.Name,
            Breed = model.Breed
        };

        _context.Dogs.Add(entity);
        var numberOfChanges = await _context.SaveChangesAsync();

        return numberOfChanges == 1;
    }

    public async Task<IEnumerable<DogDetail>> GetAllDogsAsync(int id)
    {
        var dogs = await _context.Dogs
        .Where(entity => entity.OwnerID == id)
        .Select(entity => new DogDetail
        {
            OwnerID = entity.OwnerID,
            Name = entity.Name,


        })
         .ToListAsync();

        return dogs;

    }
}