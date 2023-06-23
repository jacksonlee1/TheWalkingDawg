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
    [Route("api/[controller]")]
    public class RatingsController : Controller
    {
        private readonly ILogger<RatingsController> _logger;
        private readonly IRatingService _service;
        public RatingsController(ILogger<RatingsController> logger, IRatingService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRating([FromBody] CreateRating model)
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
            return Ok(await _service.GetRatingsAsync());
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRatingById([FromRoute] int Id)
        {
            var res = await _service.DeleteRatingByIdAsync(Id);
            return res ? Ok("User Deleted Successfully") : NotFound("Could Not Find User with Id" + Id);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRating([FromBody] UpdateRating req)
        {
            var res = await _service.UpdateRatingAsync(req);
            return res ? Ok("User Updated Successfully") : NotFound("Could Not Find Rating with Id" + req.Id);
        }
    }
}