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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalkByDogId([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _walksService.GetWalkByDogIdAsync(id));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] int Id)
        {
            var walk = await _walksService.DeleteWalkByIdAsync(Id);

            if (!walk)
            {
                return BadRequest("Unable to cancel your walking appointment.");
            }
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] int Id)
        {
            var update = await _walksService.UpdateWalkAsync(Id);

            if (!update)
            {
                return BadRequest("Walk appointment wasn't able to update.");
            }
            return Ok();
        }
    }
}