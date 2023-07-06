﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
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
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _usersService;
        private readonly ProjectsService _projectsService;

        public ProjectsController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
            _projectsService = new ProjectsService(db);
        }
        [HttpGet]
        public async Task<IEnumerable<ProjectModel>> Get()
        {
            return await _db.Projects.Select(p => p.ToDto()).ToListAsync();
        }
        [HttpPost]
        public IActionResult Create([FromBody] ProjectModel projectModel)
        {
            if (projectModel != null)
            {
                var user = _usersService.GetUser(HttpContext.User.Identity.Name);
                if (user != null)
                {
                    var admin = _db.ProjectAdmins.FirstOrDefault(a => a.UserId == user.Id);
                    if (admin == null)
                    {
                        admin = new ProjectAdmin(user);
                        _db.ProjectAdmins.Add(admin);
                    }
                    projectModel.AdminId = admin.Id;
                }

                bool result = _projectsService.Create(projectModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }

        [HttpPatch]
        public IActionResult Update(int id, [FromBody] ProjectModel projectModel)
        {
            if (projectModel != null)
            {
                bool result = _projectsService.Update(id, projectModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool result = _projectsService.Delete(id);
            return result ? Ok() : NotFound();
        }


    }
}
