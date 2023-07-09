﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Models
{
    public class Desk : CommonObject
    {
        public int Id { get; set; }
        public bool IsPrivate { get; set; }
        public string Columns { get; set; }
        public int AdminId { get; set; }
        public User Admin { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();

        public Desk() { }

        public Desk(DeskModel deskModel) : base(deskModel)
        {
            Id = deskModel.Id;
            AdminId = deskModel.AdminId;
            IsPrivate = deskModel.IsPrivate;
            ProjectId = deskModel.ProjectId;
            if (deskModel.Columns.Any())
                Columns = JsonConvert.SerializeObject(deskModel.Columns);
        }


        public DeskModel ToDto()
        {
            return new DeskModel()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                AdminId = this.AdminId,
                CreationDate = this.CreationDate,
                Photo = this.Photo,
                IsPrivate = this.IsPrivate,
                Columns = JsonConvert.DeserializeObject<string[]>(this.Columns),
                ProjectId = this.ProjectId

            };
        }

    }





}
