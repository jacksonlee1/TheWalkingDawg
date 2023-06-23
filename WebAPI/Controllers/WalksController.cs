using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Models.Walks;
using Services.Walks;

namespace WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IWalksService _walksService;
        public WalksController(ApplicationDbContext db, IWalksService walksService)
        {
            _db = db;
            _walksService = walksService;
        }
        [HttpPost]
        public async Task<IActionResult> NewWalk([FromBody] CreateWalks req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var res = await _walksService.NewWalkAsync(req);
            if (res) return Ok();
            return BadRequest();
        }
        [HttpGet("dog/{id}")]
        public async Task<IActionResult> GetWalkByDogId([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _walksService.GetWalkByDogIdAsync(id));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalkById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _walksService.GetWalkByIdAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetWalksByCurrentUser()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _walksService.GetWalksByCurrentIdAsync());
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllWalks()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _walksService.GetAllWalksAsync());
        }
        [HttpGet("New")]
        public async Task<IActionResult> GetNewWalksByCurrentUser()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _walksService.GetAvailableWalksByCurrentIdAsync());
        }
        [HttpGet("Ongoing")]
        public async Task<IActionResult> GetOngoingWalksByCurrentUser()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _walksService.GetOngoingWalksByCurrentIdAsync());
        }
        [HttpGet("Finished")]
        public async Task<IActionResult> GetFinishedWalks()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _walksService.GetFinishedWalksByCurrentIdAsync());
        }
        [HttpPut("Start/{id}")]
        public async Task<IActionResult> StartWalk([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var res = await _walksService.StartWalkByIdAsync(id);
            if (res) return Ok();
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] int Id)
        {
            var walk = await _walksService.DeleteWalkByIdAsync(Id);
            if (!walk)
            {
                return BadRequest("Unable to cancel your walking appointment.");
            }
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateWalk([FromBody] WalksUpdate req)
        {
            var update = await _walksService.UpdateWalkAsync(req);
            if (!update)
            {
                return BadRequest("Walk appointment wasn't able to update.");
            }
            return Ok();
        }
        [HttpPut("Finish")]
        public async Task<IActionResult> FinishWalk([FromBody] FinishWalk req)
        {
            var dog = await _walksService.FinishWalkAsync(req);
            if (!dog)
            {
                return BadRequest("Could not confirm walk finished.");
            }
            return Ok();
        }
    }
}