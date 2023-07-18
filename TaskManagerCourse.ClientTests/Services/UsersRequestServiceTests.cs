using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Services
{
    [TestClass()]
    public class UserRequestServiceTests
    {
        [TestMethod()]
        public void GetTokenTest()
        {
            var token = new UsersRequestService().GetToken("fedfed95@yandex.ru", "qwerty");
            Console.WriteLine(token.access_token);
            Assert.IsNotNull(token);
        }

        [TestMethod()]
        public void CreateUserTest()
        {
            var servise = new UsersRequestService();
            var token = servise.GetToken("fedfed95@yandex.ru", "qwerty");
             UserModel userTest = new UserModel("Marusia", "Sidorova", "mail@mail.ru", "qwerty", UserStatus.User, "5555");

            var result = servise.CreateUser(token, userTest);
            
            Assert.AreEqual(HttpStatusCode.OK,result);
        }

        [TestMethod()]
        public void GetAllUsersTest()
        {
            var servise = new UsersRequestService();
            var token = servise.GetToken("fedfed95@yandex.ru", "qwerty");

            var result = servise.GetAllUsers(token);

            Console.WriteLine(result.Count());

            Assert.AreNotEqual(Array.Empty<UserModel>(), result.ToArray());
        }

        [TestMethod()]
        public void DeleteUsersTest()
        {
            var servise = new UsersRequestService();
            var token = servise.GetToken("fedfed95@yandex.ru", "qwerty");

            var result = servise.DeleteUser(token, 6);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void CreateMultipleUsersTest()
        {
            var servise = new UsersRequestService();
            var token = servise.GetToken("fedfed95@yandex.ru", "qwerty");
            UserModel userTest1 = new UserModel("Marusia", "Sidorova", "mail@mail.ru", "qwerty", UserStatus.User, "5555");
            UserModel userTest2 = new UserModel("Alex", "Sidorov", "mail@mail.ru", "qwerty", UserStatus.User, "5555");
            UserModel userTest3 = new UserModel("Victor", "Sidorov", "mail@mail.ru", "qwerty", UserStatus.User, "5555");
            List<UserModel> users = new List<UserModel>() {userTest1,userTest2,userTest3};
            var result = servise.CreateMultipleUsers(token, users);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateUsersTest()
        {
            var servise = new UsersRequestService();
            var token = servise.GetToken("fedfed95@yandex.ru", "qwerty");
            UserModel userTest = new UserModel("Alex", "Sidorov222", "mail@mail22.ru", "qwerty22", UserStatus.User, "5111");
            userTest.Id = 9;
            var result = servise.UpdateUser(token, userTest);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}