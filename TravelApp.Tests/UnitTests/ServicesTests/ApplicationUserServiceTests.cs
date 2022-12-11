using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data;
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
            Assert.That(idActual, Is.EqualTo(idSearched));
        }


        [Test]
        public void Test_ApplicationUserService_GetApplicaionUserByIdNull()
        {
            //Arrange 
            var idExpected = "TestuserIdNull";

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await applicationUserService.GetApplicaionUserById(idExpected));
        }

        [Test]
        public void Test_ApplicationUserService_GetApplicaionUserByIdNull_2()
        {
            //Arrange 
            var idExpected = "Null";

            //Act 
            //Assert :throw ArgumentNullException
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
            Assert.That(users.Count(), Is.EqualTo(usersCount));
        }

        [Test]
        public void Test_ApplicationUserService_GetApplicationVIPUsers()
        {
            //Arange
            int VIPUsersCount = data.Users.Where(u => u.IsVIP == true).Count();

            //Act : get all VIP users
            var VIPusers = applicationUserService.GetApplicationVIPUsers().Result;

            //Assert : number of VIP users is correct
            Assert.That(VIPusers.Count(), Is.EqualTo(VIPUsersCount));
        }

       

        [Test]
        public void Test_ApplicationUserService_MakeVIPNull()
        {
            //Arange
            string userId = "TestuserId1Null";

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await applicationUserService.MakeVIP(userId));
        }


        [Test]
        public void Test_ApplicationUserService_MakeVIPNull_2()
        {
            //Arange
            string userId = "";

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await applicationUserService.MakeVIP(userId));
        }

        [Test]
        public void Test_ApplicationUserService_RemoveVIPNull()
        {
            //Arange
            string userId = "TestuserId1Null";

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await applicationUserService.RemoveVIP(userId));
        }

        [Test]
        public void Test_ApplicationUserService_RemoveVIPNull_2()
        {
            //Arange
            string userId = "";

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await applicationUserService.RemoveVIP(userId));
        }

    }
}
