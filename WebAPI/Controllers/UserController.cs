using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Token;
using Models.User;
using Services.Token;
using Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ITokenService _tokenService;
        public UserController(IUserService service, ITokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegister user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.RegisterUserAsync(user);
            if (result)
            {
                return Ok("User Registered Succesfully");
            }
            return BadRequest("User could not be registered");
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet]

        public async Task<IActionResult> GetCurrentUser()

        {
            var res = await _service.GetUserByCurrentUserAsync();
            return (res != null)?Ok(res):NotFound("Could not find user");
        }

        [HttpGet("Sort/Descending")]
        public async Task<IActionResult> SortUsersDescending()
        {
            var users = await _service.SortWalkersByAverageRating(true);
            return Ok(users);
        }

        [HttpGet("Sort/Ascending")]
        public async Task<IActionResult> SortUsersAscending()
        {
            var users = await _service.SortWalkersByAverageRating(false);
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("User Not Found");
        }

        [HttpPost("~/api/Token")]
        public async Task<IActionResult> Token([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tokenResponse = await _tokenService.GetTokenAsync(request);
            if (tokenResponse is null)
                return BadRequest("Invalid Username or Password");
            return Ok(tokenResponse);
        }



        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]UserUpdate req)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var res = await _service.UpdateUserAsync(req);
            return res?Ok("User Updated Sucessfully"):NotFound("Could not Update User");
        }
         [HttpPut("Update")]
        public async Task<IActionResult> UpdateCurentUser([FromBody] UserUpdate req)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var res = await _service.UpdateCurrentUserAsync(req);
            return res?Ok("User Updated Sucessfully"):NotFound("Could not Update User");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute]int id)
        {
            var res = await _service.DeleteUserByIdAsync(id);
            return res?Ok("User Deleted Sucessfully"):NotFound("Could not Delete User");
        }

         [HttpDelete]
        public async Task<IActionResult> DeleteCurrentUser([FromRoute]int id)
        {
            
            var res = await _service.DeleteUserByIdAsync(id);
            return res?Ok("User Deleted Sucessfully"):NotFound("Could not Delete User");
        
        }

    }
}
