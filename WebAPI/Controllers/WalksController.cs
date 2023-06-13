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
    public class WalksController
    {
        private readonly ApplicationDbContext _db;
        private readonly IWalksService _walksService;

        public WalksController(ApplicationDbContext db, IWalksService walksService)
        {
            _db = db;
            _walksService = walksService;
        }

        [HttpPost]
        public async Task<IActionResult> NewWalk([FromBody]CreateWalks req)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var res = await _walksService.NewWalkAsync(req);
            if(res) return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalkByDogId([FromRoute]int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _walksService.GetWalkByDogIdAsync(id));

        }
    }
}