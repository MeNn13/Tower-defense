using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TowerDefenseAPI.Domain.Models;
using TowerDefenseAPI.Service.Interfaces;

namespace TowerDefenseAPI.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userRepository) => _userService = userRepository;

        private IActionResult StatusCode(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                return Ok(statusCode);
            }

            if (statusCode == HttpStatusCode.NotFound)
                return NotFound();

            return BadRequest();
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SingUp(User user)
        {
            var response = await _userService.Create(user);
            return StatusCode(response.StatusCode);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAll()
        {
            var response = await _userService.GetAll();
            StatusCode(response.StatusCode);
            return response.Data;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("del")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _userService.Delete(id);
            return StatusCode(response.StatusCode);
        }   
    }
}
