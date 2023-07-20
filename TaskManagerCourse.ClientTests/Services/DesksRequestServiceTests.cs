using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagerCourse.Client.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Common.Models;
using System.Net;

namespace TaskManagerCourse.Client.Services.Tests
{
    [TestClass()]
    public class DesksRequestServiceTests
    {

        private AuthToken _token;
        private DesksRequestService _service;

        public DesksRequestServiceTests()
        {
            _token = new UsersRequestService().GetToken("fedfed95@yandex.ru", "qwerty");
            _service = new DesksRequestService();
        }

        [TestMethod()]
        public void GetAllDesksTest()
        {
            var desks = _service.GetAllDesks(_token);

            Console.WriteLine(desks.Count);

            Assert.AreNotEqual(Array.Empty<ProjectModel>(), desks);
        }

        [TestMethod()]
        public void GetDesksByIdTest()
        {
            var desk = _service.GetDesksById(_token, 2);

            Assert.AreNotEqual(null, desk);

        }

        [TestMethod()]
        public void GetDeskByProjectTest()
        {
            var desks = _service.GetDeskByProject(_token, 2);
            Assert.AreNotEqual(0, desks.Count);
        }

        [TestMethod()]
        public void CreateDeskTest()
        {
            var desk = new DeskModel("Доска из тестов", "обычная доска для тестирования сервисов", true, new string[] {"Новые", "Готовые"});
            desk.ProjectId = 2;
            desk.AdminId = 1;
            var result = _service.CreateDesk(_token, desk);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateDeskTest()
        {
            var desk = new DeskModel("Доска11 из тестов", "обычная11 доска для тестирования сервисов", true, new string[] { "Новые","На проверке", "Готовые" });
            desk.ProjectId = 2;
            desk.AdminId = 1;
            desk.Id = 5;
            var result = _service.UpdateDesk(_token, desk);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteDeskTest()
        {
            var result = _service.DeleteDesk(_token, 3);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}