using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}