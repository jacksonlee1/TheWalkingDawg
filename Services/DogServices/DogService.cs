
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
    public DogService(IHttpContextAccessor httpContext, ApplicationDbContext context)
    {
        _context = context;

        var userClaims = httpContext.HttpContext.User.Identity as ClaimsIdentity;//getting the user claims
        var value = userClaims?.FindFirst("Id")?.Value;
        var validId = int.TryParse(value, out _userId);
        if (!validId)
        {
            throw new Exception("Attempted to build DogService without User Id Claim");
        }
    }

    public async Task<bool> CreateDogAsync(DogCreate model)
    {
        var entity = new DogsEntity
        {
            OwnerId = _userId,
            Name = model.Name,
            Breed = model.Breed
        };

        _context.Dogs.Add(entity);
        var numberOfChanges = await _context.SaveChangesAsync();

        return numberOfChanges == 1;
    }

    public async Task<IEnumerable<DogDetail>> GetAllDogsAsync()
    {
        var dogs = await _context.Dogs
        var dogs = await _context.Dogs.Include(d => d.Owner)
        .Select(entity => new DogDetail
        {
            OwnerId = entity.OwnerId,
            Id = entity.Id,
            Name = entity.Name,
            Breed = entity.Breed,
            Username = entity.Owner.Username
        })
        .ToListAsync();
        return dogs;
    }
    }

    public async Task<IEnumerable<DogsEntity>> GetDogsByCurrentUserAsync()
    {
        return await _context.Dogs.Where(d => d.Id == _userId).ToListAsync();
    }
    public async Task<IEnumerable<DogsEntity>> GetDogByOwnerIdAsync(int id)
    {
        //returning the dog from db that matches with ownerId
        return await _context.Dogs.Where(d => d.Id == id).ToListAsync();
    }

    //ToDo(Stretch): Get Dogs By Walking Time
    public async Task<IEnumerable<DogsEntity>> GetDogsByWalkingTimeAsync(int WalkingTime)
    {
        return await _context.Dogs.Where(d =>d.WalkingTime == WalkingTime).ToListAsync();
    }
    //ToDo(Stretch): Get Dogs By Walking Distance

    public async Task<DogsEntity?> GetDogByIdAsync(int id)
    {
        return await _context.Dogs.FindAsync(id);
    }

    public async Task<bool> UpdateDogAsync(DogUpdate request)
    {
        var entity = await _context.Dogs.FindAsync(request.Id);

        //Updating the entity's properties
        entity.Name = request.Name;
        entity.Breed = request.Breed;
        entity.ReqDistance = request.ReqDistance;
        entity.WalkingTime = request.WalkingTime;

        //Save the changes to database and capture how many rows have been updated
        var numberOfChanges = await _context.SaveChangesAsync();

        return numberOfChanges == 1;
    }

    public async Task<bool> DeleteDogByIdAsync(int id)
    {
        var entity = await _context.Dogs.FindAsync(id);
        _context.Dogs.Remove(entity);

        var numberOfChanges = await _context.SaveChangesAsync();

        return numberOfChanges == 1;
    }
}