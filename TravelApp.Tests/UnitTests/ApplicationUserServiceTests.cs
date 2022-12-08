using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Repositories;
using TravelApp.Tests.Mocks;
using TravelApp.Tests.UnitTests;

namespace TravelApp.Tests.UnitTests
{
    [TestFixture]
    public class ApplicationUserServiceTests : UnitTestsBase
    {
        private IApplicationUserService applicationUserService;


        [OneTimeSetUp]
        public void SetUp()
        {
            var repo = new Repository(this.data);

            this.applicationUserService = new ApplicationUserService(repo);
        }

        [Test]
        public void Test_ApplicationUserService_Delete()
        {
            //Arrange
            //Act : deletes the user with give id
            string id = "TestuserId";

            applicationUserService.Delete(id);

            //Assert : number of users is correct
            Assert.That(this.data.Users.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Test_ApplicationUserService_GetApplicaionUserById()
        {
            //Arrange
            //Act : deletes the user with give id
            string id = "TestuserId";
            var applicationUser = applicationUserService.GetApplicaionUserById(id);

            //Assert : number of users is correct
            Assert.That(applicationUser.Id, Is.EqualTo(id));
        }
    }
}
