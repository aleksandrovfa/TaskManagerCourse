using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagerCourse.Client.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerCourse.Common.Models;
using TaskManagerCourse.Client.Models;
using System.Net;

namespace TaskManagerCourse.Client.Services.Tests
{
    [TestClass()]
    public class ProjectRequestServiceTests
    {
        private AuthToken _token;
        private ProjectsRequestService _service;

        public ProjectRequestServiceTests()
        {
            _token = new UsersRequestService().GetToken("fedfed95@yandex.ru", "qwerty");
            _service = new ProjectsRequestService();
        }

        [TestMethod()]
        public void GetAllProjectsTest()
        {

            var projects = _service.GetAllProjects(_token);

            Console.WriteLine(projects.Count);

            Assert.AreNotEqual(Array.Empty<ProjectModel>(), projects);
        }


        [TestMethod()]
        public void GetProjectByIdTest()
        {

            var project = _service.GetProjectsById(_token, 1);

            Assert.AreNotEqual(null, project);
        }

        [TestMethod()]
        public void CreateProjectTest()
        {
            ProjectModel project = new ProjectModel("Тестовый проект", "Новый тестовый проект созданный из тестов", ProjectStatus.inProgress);
            project.AdminId = 1;
            var result = _service.CreateProject(_token, project);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateProjectTest()
        {
            ProjectModel project = new ProjectModel("Тестовый 111проект обнов", "Новый 11обнов тестовый проект созданный из тестов", ProjectStatus.Suspended);
            project.Id = 1;
            var result = _service.UpdateProject(_token, project);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteProjectTest()
        {
            var result = _service.DeleteProject(_token, 3);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void AddUsersToProjectTest()
        {
            var result = _service.AddUsersToProject(_token, 2, new List<int>() { 5,6});
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void RemoveUsersFromProjectTest()
        {
            var result = _service.RemoveUsersFromProject(_token, 2, new List<int>() { 6 });
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}