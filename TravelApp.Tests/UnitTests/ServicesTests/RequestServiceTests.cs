using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.RequestModels;
using TravelApp.Data.Models.TownModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class RequestServiceTests : UnitTestsBase
    {
        private IRequestService requestService;
        private IRepository repository;


        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);

            requestService = new RequestService(repository);
        }


        [Test]
        public void Test_RequestService_Add()
        {
            //Arrange
            int journeyId = 1;
            string userId = "TestuserId";
            int requestsCount = this.data.Requests.Count();

            //Act : create and add new request
            var requestToAdd = new AddRequestModel()
            {
                NumberOfPeople = 2,
                JourneyId = 1,
                ApplicationUserId = "TestuserId",           
            };

            requestService.Add(requestToAdd,userId, journeyId);

            //Assert : new town is added
            Assert.That(this.data.Requests.Count() == requestsCount + 1);
        }

        [Test]
        public void Test_RequestService_AddNullUser()
        {
            //Arange
            int journeyId = 1;
            string userId = "notInvalid";
            var requestToAdd = new AddRequestModel()
            {
                NumberOfPeople = 2,
                JourneyId = 1,
                ApplicationUserId = "TestuserId",
            };

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await requestService.Add(requestToAdd,  userId, journeyId));
        }

        [Test]
        public void Test_RequestService_AddNullJourney()
        {
            //Arange
            int journeyId = 100000;
            string userId = "TestuserId";
            var requestToAdd = new AddRequestModel()
            {
                NumberOfPeople = 2,
                JourneyId = 1,
                ApplicationUserId = "TestuserId",
            };

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await requestService.Add(requestToAdd, userId, journeyId));
        }

        [Test]
        public void Test_RequestService_Approve()
        {
            //Arrange
            int requestId = 3;

            //Act
            requestService.Approve(requestId);
            var request = requestService.GetRequestById(requestId).Result;

            //Assert
            Assert.That(request.IsApproved == true);
        }
        [Test]
        public void Test_RequestService_ApproveNullRequest()
        {
            //Arrange
            int requestId = 300000;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await requestService.Approve(requestId));
        }
        [Test]
        public void Test_RequestService_Decline()
        {
            //Arrange
            int requestId = 2;

            //Act
            requestService.Decline(requestId);
            var request = requestService.GetRequestById(requestId).Result;

            //Assert
            Assert.That(request.IsApproved == false);
        }
        [Test]
        public void Test_RequestService_DeclineNullRequest()
        {
            //Arrange
            int requestId = 300000;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await requestService.Decline(requestId));
        }


        [Test]
        public void Test_RequestService_GetRequestById()
        {
            //Arange
            int requestId = 1;

            //Act : get request
            var request = requestService.GetRequestById(requestId).Result;

            //Assert : request properties are correct
            Assert.That(request.Id == 1);
            Assert.That(request.NumberOfPeople == 2);
            Assert.That(request.JourneyId == 1);
            Assert.That(request.ApplicationUserId == "TestuserId");
            Assert.That(request.IsApproved == false);
            Assert.That(request.IsManaged == false);

        }

        [Test]
        public void Test_RequestService_GetAllRequests()
        {
            //Arange
            int requestsCount = data.Requests.Count();

            //Act : get all requests
            var requests = requestService.GetAllRequests().Result;

            //Assert : number of requests is correct
            Assert.That(requests.Count() == requestsCount);
        }

        [Test]
        public void Test_RequestsService_GetMyRequests()
        {
            //Arange
            string userId = "TestuserId";
            int myRequestsCount = data.Requests.Where(r => r.ApplicationUserId == userId).Count();

            //Act : get all my requests
            var requests = requestService.GetMyRequests(userId).Result;

            //Assert : number of my requests is correct
            Assert.That(requests.Count() == myRequestsCount);
        }

    }
}