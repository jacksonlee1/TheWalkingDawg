//Controllers handle HTTP requests.  DogController will handle any HTTP requests that pertains to Dog

using Microsoft.AspNetCore.Mvc;
using Models.Dogs;
using Services.DogServices;

namespace WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]//all endpoints in this controller will start with api/Dog


public class DogController : ControllerBase
{

    //_dogService field added for the IDogService interface
    //this is so the methods/endpoints can use the injected service
    private readonly IDogService _dogService;
    //DogController constructor with IDogService parameter
    public DogController(IDogService dogService)
    {
        _dogService = dogService;
    }

    [HttpPost("Create")]// create a new dog entry

    public async Task<IActionResult> CreateDog([FromBody] DogCreate model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createResult = await _dogService.CreateDogAsync(model);
        if (createResult)
        {
            return Ok("New dog entry was successfully created.");
        }
        return BadRequest("New dog entry could not be created.");
    }

    [HttpGet]//get all dogs

    public async Task<IActionResult> GetAllDogs()
    {
        var dogs = await _dogService.GetAllDogsAsync();
        return Ok(dogs);
    }

    [HttpGet("{id}")]//Get dog by Id

    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var dogDetail = await _dogService.GetDogByIdAsync(id);

        if (dogDetail is null)
        {
            return NotFound();
        }
        return Ok(dogDetail);
    }

    [HttpGet("Current")]

    public async Task<IActionResult> GetDogsByCurrentUser()
    {
        var dogByUser = await _dogService.GetDogsByCurrentUserAsync();

        if (dogByUser is null)
        {
            return NotFound();
        }
        return Ok(dogByUser);
    }

    [HttpGet("~/api/admin/dog/{id}")]//Get dog by Owner's Id

    public async Task<IActionResult> GetDogByOwnerId([FromRoute] int id)
    {
        //getting the dogByOwner Id from the service
        var dogByOwner = await _dogService.GetDogByOwnerIdAsync(id);

        if (dogByOwner is null)
        {
            return NotFound();
        }
        return Ok(dogByOwner);
    }

    [HttpGet("time/{WalkingTime}")]//Get dog by Walking Time

    public async Task<IActionResult> GetDogsByWalkingTime([FromRoute] int WalkingTime)
    {
        var dogsByWalkingTime = await _dogService.GetDogsByWalkingTimeAsync(WalkingTime);

        if (dogsByWalkingTime is null)
        {
            return NotFound();
        }
        return Ok(dogsByWalkingTime);
    }

    [HttpPut]//takes in the DogUpdate 

    public async Task<IActionResult> UpdateDogById([FromBody] DogUpdate request)
    {
        //validating the model, if isn't valid returns the bad modelstate
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        //passing the request to the service where it will attempt to update the dog
        return await _dogService.UpdateDogAsync(request)
            ? Ok("Dog updated successfully.")
            : BadRequest("Dog could not be updated");
    }

    [HttpDelete("{id}")] //Delete a dog
    public async Task<IActionResult> DeleteDog([FromRoute] int id)
    {
        var dog = await _dogService.DeleteDogByIdAsync(id);

        if (!dog)
        {
            return BadRequest("Could not delete the dog.");
        }
        return Ok();
    }
}
