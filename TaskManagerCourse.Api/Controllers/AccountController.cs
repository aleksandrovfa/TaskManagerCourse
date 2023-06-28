using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TaskManagerCourse.Api.Models.Data;

namespace TaskManagerCourse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _db;
        public AccountController(ApplicationContext db)
        {
            _db = db;
        }
        [HttpGet("info")]
        public IActionResult GetCurrentUserInfo()
        {
            string username = HttpContext.User.Identity.Name;
            var user = _db.Users.FirstOrDefault(x => x.Email == username);

            if (user != null)
                return Ok(user.ToDto());
            return NotFound();
        }

        public IActionResult GetToken()
        {

        }
    }
}
