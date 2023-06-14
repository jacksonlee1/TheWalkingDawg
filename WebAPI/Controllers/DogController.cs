
using Microsoft.AspNetCore.Mvc;
using Models.Dogs;
using Services.DogServices;

namespace WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]


public class DogController : ControllerBase
{

    private readonly IDogService _dogService;
    public DogController(IDogService dogService)
    {
        _dogService = dogService;
    }

[HttpPost("Create")]// create a new dog entry

public async Task<IActionResult>CreateDog([FromBody]DogCreate model)
{
    if(!ModelState.IsValid)
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

public async Task<IActionResult>GetAllDogs()
{
    var dogs = await _dogService.GetAllDogsAsync();
    return Ok(dogs);
}

[HttpGet ("{id:int}")]//Get dog by ID

public async Task<IActionResult>GetById([FromRoute]int id)
{
    var dogDetail = await _dogService.GetDogByIdAsync(id);

    if(dogDetail is null)
    {
        return NotFound();
    }
    return Ok(dogDetail);
}

[HttpDelete ("{id:int}")] //Delete a dog
public async Task<IActionResult>DeleteDog([FromRoute] int id)
{
    var dog = await _dogService.DeleteDogByIdAsync(id);

    if (!dog)
    {
        return BadRequest ("Could not delete the dog.");
    }
        return Ok();

}
}
