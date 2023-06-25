using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerCourse.Api.Models;
using TaskManagerCourse.Api.Models.Data;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public UsersController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {
            if(userModel != null)
            {
                User newUser = new User(userModel.FirstName, userModel.LastName,userModel.Email, 
                    userModel.Password, userModel.Status, userModel.Phone, userModel.Photo);
                _db.Users.Add(newUser);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("test")]
        public IActionResult TestApi() 
        { 
            return Ok("Всем привет!");
        }
    }
}
