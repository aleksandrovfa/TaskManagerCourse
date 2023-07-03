using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCourse.Api.Models;
using TaskManagerCourse.Api.Models.Data;
using TaskManagerCourse.Api.Models.Services;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UserService _usersService;
        

        public UsersController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UserService(db);
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult TestApi()
        {
            return Ok("Всем привет!");
        }

       
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                bool result = _usersService.Create(userModel);
                return result ?Ok() : NotFound();
            }
            return BadRequest();
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                bool result = _usersService.Update(id, userModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }

        

        [HttpDelete("{id}")]
        public IActionResult DeleteUser (int id)
        {
            bool result = _usersService.Delete(id);
            return result ? Ok() : NotFound();
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _db.Users.Select(x => x.ToDto()).ToListAsync();
        }

        [HttpPost("create/all")]
        public async Task<IActionResult> CreateMultipleUsers([FromBody] List<UserModel> userModels)
        {
            if (userModels != null && userModels.Count > 0)
            {
                bool result = _usersService.CreateMultipleUsers(userModels);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }
    }
}
