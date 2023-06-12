
using Microsoft.AspNetCore.Mvc;
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

}
