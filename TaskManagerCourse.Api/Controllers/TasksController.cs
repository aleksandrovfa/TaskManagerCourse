using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerCourse.Api.Models.Data;
using TaskManagerCourse.Api.Models.Services;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private readonly ApplicationContext _db;
        private readonly UsersService _usersService;
        private readonly TasksService _tasksService;

        public TasksController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
            _tasksService = new TasksService(db);
        }

        [HttpGet]
        public async Task<IEnumerable<CommonModel>> GetTasksByDesk(int deskId)
        {
            return await _tasksService.GetAll(deskId).ToListAsync();
        }

        [HttpGet("user")]
        public async Task<IEnumerable<CommonModel>> GetTasksForCurrentUser()
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                return await _tasksService.GetTasksForUser(user.Id).ToListAsync();
            }
            return Array.Empty<CommonModel>();
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var task = _tasksService.Get(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskModel taskModel)
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                if (taskModel != null)
                {
                    taskModel.CreatorId = user.Id;
                    bool result = _tasksService.Create(taskModel);
                    return result ? Ok() : NotFound();

                }
                return BadRequest();
            }
            return Unauthorized();
        }


        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] TaskModel model)
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                if (model != null)
                {
                    bool result = _tasksService.Update(id, model);
                    return result ? Ok() : NotFound();

                }
                return BadRequest();
            }
            return Unauthorized();
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = _tasksService.Delete(id);
            return result ? Ok() : NotFound();
        }
    }
}
