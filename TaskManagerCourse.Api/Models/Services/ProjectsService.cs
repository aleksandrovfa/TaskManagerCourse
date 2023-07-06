using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerCourse.Api.Models.Abstractions;
using TaskManagerCourse.Api.Models.Data;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Models.Services
{
    public class ProjectsService : AbstractionService, ICommonService<ProjectModel>
    {
        private readonly ApplicationContext _db;

        public ProjectsService(ApplicationContext db)
        {
            _db = db;
        }


        public bool Create(ProjectModel model)
        {
            bool result = DoAction(delegate ()
            {
                Project newProject = new Project(model);
                _db.Projects.Add(newProject);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                Project newProject = _db.Projects.FirstOrDefault(p => p.Id == id);
                _db.Projects.Remove(newProject);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Update(int id, ProjectModel model)
        {

            bool result = DoAction(delegate ()
            {
                Project newProject = _db.Projects.FirstOrDefault(p => p.Id == id);
                newProject.Name = model.Name;
                newProject.Description = model.Description;
                newProject.Photo = model.Photo;
                newProject.Status = model.Status;
                newProject.AdminId = model.AdminId;
                _db.Projects.Update(newProject);
                _db.SaveChanges();
            });
            return result;


            
        }
    }
}
