using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using TaskManagerCourse.Api.Models.Abstractions;
using TaskManagerCourse.Api.Models.Data;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Models.Services
{
    public class DesksService : AbstractionService, ICommonService<DeskModel>
    {
        private readonly ApplicationContext _db;
        public DesksService(ApplicationContext db)
        {
            _db = db;
        }


        public bool Create(DeskModel model)
        {
            bool result = DoAction(delegate ()
            {
                Desk desk = new Desk(model);
                _db.Desks.Add(desk);
                _db.SaveChanges();
            });
            return result;

        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                Desk desk = _db.Desks.FirstOrDefault(d => d.Id == id);
                _db.Desks.Remove(desk);
                _db.SaveChanges();
            });
            return result;


        }

        public DeskModel Get(int id)
        {
            Desk desk = _db.Desks
                .Include(x => x.Tasks)
                .FirstOrDefault(x => x.Id == id);
            var deskModel = desk?.ToDto();
            if (deskModel != null)
            {
                deskModel.TasksIds = desk.Tasks.Select(x => x.Id).ToList();
            }
            return deskModel;
        }

        public bool Update(int id, DeskModel model)
        {
            bool result = DoAction(delegate ()
            {
                Desk desk = _db.Desks.FirstOrDefault(d => d.Id == id);
                desk.Name = model.Name;
                desk.Description = model.Description;
                desk.Photo = model.Photo;
                desk.AdminId = model.AdminId;
                desk.IsPrivate = model.IsPrivate;
                desk.Columns = JsonConvert.SerializeObject(model.Columns);
                _db.Desks.Update(desk);
                _db.SaveChanges();
            });
            return result;
        }

        public IQueryable<CommonModel> GetAll(int userId)
        {
            return _db.Desks.Where(d => d.AdminId == userId)
                .Select(x => x.ToDto() as CommonModel);
        }

        public IQueryable<CommonModel> GetProjectDesks(int projectId, int userId)
        {
            return _db.Desks.Where(d => d.ProjectId == projectId &&
            (d.AdminId == userId || d.IsPrivate == false))
                .Select(p => p.ToDto() as CommonModel);

        }

    }
}
