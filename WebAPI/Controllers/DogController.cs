
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

[HttpPost("Create")]

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

[HttpGet]

public async Task<IActionResult>GetAllDogs()
{
    var dogs = await _dogService.GetAllDogsAsync();
    return Ok(dogs);
}
}
