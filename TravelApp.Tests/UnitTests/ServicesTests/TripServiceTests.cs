using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.PostModels;
using TravelApp.Data.Models.TripModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class TripServiceTests : UnitTestsBase
    {
        private ITripService tripService;
        private IRepository repository;


        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);

            tripService = new TripService(repository);
        }

        [Test]
        public void Test_TripService_Add()
        {
            //Arrange
            int tripsCount = this.data.Trips.Count();
            string userId = "TestuserId";

            //Act : create and add new trip
            var tripToAdd = new AddTripModel()
            {

                Title = "Test Trip Title 4",
                ApplicationUserId = "TestuserId",
                Rating = 10,
                JourneyId = 1
            };

            tripService.Add(tripToAdd, userId);

            //Assert : new trip is added
            Assert.That(this.data.Trips.Count(), Is.EqualTo(tripsCount + 1));
        }

        [Test]
        public void Test_TripService_Delete()
        {
            //Arrange
            int tripId = 3;
            int tripsCount = data.Trips.Count();

            //Act : deletes the trip with given id
            tripService.Delete(tripId);

            //Assert : number of posts is correct
            Assert.That(data.Trips.Count(), Is.EqualTo(tripsCount - 1));
        }


        [Test]
        public void Test_TripService_GetAllTrips()
        {
            //Arange
            string userId = "TestuserId";
            int tripsCount = data.Trips.Where(t => t.ApplicationUserId == userId).Count();

            //Act : get all my trips
            var trips = tripService.GetAllTrips(userId).Result;

            //Assert : number of all my trips is correct
            Assert.That(trips.Count(), Is.EqualTo(tripsCount));
        }


       
        [Test]
        public void Test_TripService_GetTripDetailsById()
        {
            //Arange
            int tripId = 1;

            //Act : get trip
            var trip = tripService.GetTripDetailsById(tripId).Result;

            Assert.Multiple(() =>
            {

                //Assert : trip properties are correct
                Assert.That(trip.Title, Is.EqualTo("Test Trip Title"));
                Assert.That(trip.Rating, Is.EqualTo(10));
            });
        }

        [Test]
        public void TestPostService_GetPostDetailsByIdNull()
        {
            //Arange
            int tripId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await tripService.GetTripDetailsById(tripId));
        }


        [Test]
        public void TestPostService_GetPostDetailsByIdNull_2()
        {
            //Arange
            int tripId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await tripService.GetTripDetailsById(tripId));
        }

        [Test]
        public void Test_TripService_GetTripById()
        {
            //Arange
            int tripId = 1;

            //Act : get trip
            var trip = tripService.GetTripById(tripId).Result;

            Assert.Multiple(() =>
            {

                //Assert : post properties are correct
                Assert.That(trip.Title, Is.EqualTo("Test Trip Title"));
                Assert.That(trip.ApplicationUserId, Is.EqualTo("TestuserId"));
                Assert.That(trip.Rating, Is.EqualTo(10));
                Assert.That(trip.JourneyId, Is.EqualTo(1));
            });
        }

        [Test]
        public void Test_CommentService_GetCommentByIdNull()
        {
            //Arange
            int tripId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await tripService.GetTripDetailsById(tripId));
        }


        [Test]
        public void Test_CommentService_GetCommentByIdNull_2()
        {
            //Arange
            int tripId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await tripService.GetTripDetailsById(tripId));
        }


        [Test]
        public void Test_TripService_GetTripsForSelect()
        {
            //Arange
            string userId = "TestuserId";
            int tripsCount = data.Trips.Where(t => t.ApplicationUserId == userId).Count();

            //Act : get all trips
            var trips = tripService.GetTripsForSelect(userId).Result;

            //Assert : number of trips is correct
            Assert.That(trips.Count(), Is.EqualTo(tripsCount));
        }


        [Test]
        public void Test_TripService_EditCreateForm()
        {
            //Arrange
            int tripId = 2;
            var trip = tripService.GetTripById(tripId).Result;

            //Act
            var tripEditForm = tripService.EditCreateForm(tripId).Result;

            Assert.Multiple(() =>
            {

                //Assert
                Assert.That(trip.Title, Is.EqualTo(tripEditForm.Title));
                Assert.That(trip.Rating, Is.EqualTo(tripEditForm.Rating));
            });
        }

        [Test]
        public void Test_TownService_DeleteCreateForm()
        {
            //Arrange
            int tripId = 4;
            var trip = tripService.GetTripById(tripId).Result;

            //Act
            var tripDeleteForm = tripService.DeleteCreateForm(tripId).Result;

            Assert.Multiple(() =>
            {

                //Assert
                Assert.That(trip.Id, Is.EqualTo(tripDeleteForm.Id));
                Assert.That(trip.Title, Is.EqualTo(tripDeleteForm.Title));
                Assert.That(trip.Rating, Is.EqualTo(tripDeleteForm.Rating));
            });
        }



        [Test]
        public void Test_TripService_GetTrips()
        {
            //Arange
            int tripsCount = data.Trips.Count();

            //Act : get all trips of all users
            var trips = tripService.GetTrips().Result;

            //Assert : number of trips is correct
            Assert.That(trips.Count(), Is.EqualTo(tripsCount));
        }



    }
}
