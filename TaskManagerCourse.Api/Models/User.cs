﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime RedistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public byte[] Photo { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Desk> Desks { get; set; } = new List<Desk>();
        public List<Task> Tasks { get; set; } = new List<Task>();
        public UserStatus Status { get; set; }
        public User() { }
        public User(string fname, string lname, string email, string password,
            UserStatus status = UserStatus.User, string phone = null, byte[] photo = null)
        {
            FirstName = fname;
            LastName = lname;
            Email = email;
            Password = password;
            Status = status;
            Phone = phone;
            Photo = photo;
            RedistrationDate = DateTime.Now;
        }

        public UserModel ToDto()
        {
            return new UserModel()
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Password = this.Password,
                Phone = this.Phone,
                Photo = this.Photo,
                RedistrationDate = this.RedistrationDate,
                Status = this.Status,
            };
        }
    }
}
