using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Rating;
using Services.Rating;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class RatingsController : Controller
    {
        private readonly ILogger<RatingsController> _logger;
        private readonly IRatingService _service;

        public RatingsController(ILogger<RatingsController> logger, IRatingService service)
        {
            _logger = logger;
            _service = service;

        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] CreateRating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.NewRatingAsync(model);
            if (result)
            {
                return Ok("New Rating Left Successfully");
            }
            return BadRequest("Could not leave rating");
        }
        [HttpGet]

        public async Task<IActionResult> GetAllRatings()
        {
            return Ok( await _service.GetRatingsAsync());

        }

    }
}