using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Repositories;
using TravelApp.Tests.Mocks;

namespace TravelApp.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class ApplicationUserServiceTests : UnitTestsBase
    {
        private IApplicationUserService applicationUserService;
        private IRepository repository;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);

            applicationUserService = new ApplicationUserService(repository);
        }


        [Test]
        public void Test_ApplicationUserService_Delete()
        {
            //Arrange
            string userId = "ApplicationUserDelete";
            int usersCount = data.Users.Count();

            //Act : deletes the user with given id
            applicationUserService.Delete(userId);

            //Assert : number of users is correct
            Assert.That(data.Users.Count(), Is.EqualTo(usersCount - 1));
        }

        [Test]
        public void Test_ApplicationUserService_GetApplicaionUserById()
        {
            //Arrange 
            var idSearched = "TestuserId";

            //Act : get a particular user with given id

            var applicationUser = applicationUserService.GetApplicaionUserById(idSearched).Result;
            var idActual = applicationUser.Id;

            //Assert : id of found user is equal to expected one
            Assert.That(idActual == idSearched);
        }


        [Test]
        public void Test_ApplicationUserService_GetApplicaionUserByIdNull()
        {
            //Arrange 
            var idExpected = "TestuserIdNull";

            //Act : get a particular user with given id
            //Assert : id of found user is equal to expected one
            Assert.ThrowsAsync<ArgumentNullException>(async () => await applicationUserService.GetApplicaionUserById(idExpected));
        }


        [Test]
        public void Test_ApplicationUserService_GetApplicationUsers()
        {
            //Arange
            int usersCount = data.Users.Count();    

            //Act : get all users
            var users = applicationUserService.GetApplicationUsers().Result;

            //Assert : number of users is correct
            Assert.That(users.Count() == usersCount);
        }

        [Test]
        public void Test_ApplicationUserService_GetApplicationVIPUsers()
        {
            //Arange
            int VIPUsersCount = data.Users.Where(u => u.IsVIP == true).Count();

            //Act : get all VIP users
            var VIPusers = applicationUserService.GetApplicationVIPUsers().Result;

            //Assert : number of VIP users is correct
            Assert.That(VIPusers.Count() == VIPUsersCount);
        }

        [Test]
        public void Test_ApplicationUserService_MakeVIP()
        {
            //Arange
            string userId = "TestuserIdNonVip";
            var user = applicationUserService.GetApplicaionUserById(userId).Result;

            //Act : make user VIP
            applicationUserService.MakeVIP(user.Id);

            //Assert : user status is VIP
            Assert.IsTrue(user.IsVIP);

        }

        [Test]
        public void Test_ApplicationUserService_MakeVIPNull()
        {
            //Arange
            string userId = "TestuserId1Null";

            //Act
            //Assert : throws Argument Null Exception
            Assert.ThrowsAsync<ArgumentNullException>(async () => await applicationUserService.MakeVIP(userId));
        }

        [Test]
        public void Test_ApplicationUserService_RemoveVIP()
        {
            //Arange
            string userId = "TestuserIdVip";

            //Act : remove user VIP Status
            applicationUserService.RemoveVIP(userId);
            var user = applicationUserService.GetApplicaionUserById(userId).Result;

            //Assert : user status is not VIP
            Assert.That(user.IsVIP == false);
        }

        [Test]
        public void Test_ApplicationUserService_RemoveVIPNull()
        {
            //Arange
            string userId = "TestuserId1Null";

            //Act
            //Assert : throws Argument Null Exception
            Assert.ThrowsAsync<ArgumentNullException>(async () => await applicationUserService.RemoveVIP(userId));
        }

    }
}
