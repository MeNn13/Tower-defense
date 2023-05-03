using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TowerDefenseAPI.Data;
using TowerDefenseAPI.Domain.Models;

namespace TowerDefenseAPI.Controllers
{
    [ApiController]
    [Route("SignUp")]
    public class SignUp : Controller
    {

        ApplicationDbContext context;
        public SignUp (ApplicationDbContext _context) => context = _context;

        [HttpPost]
        public IActionResult SingUp(User user)
        {

            return Ok(context.Users.ToList());
        }
    }
}
