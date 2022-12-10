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
            Assert.That(this.data.Trips.Count() == tripsCount + 1);
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
        public void Test_TripService_Edit()
        {
            //Arrange
            int tripId = 2;
            var trip = this.data.Trips.Where(t => t.Id == tripId).First();

            //Act : edit a trip
            var tripToEdit = new EditTripModel()
            {
                Id = 2,
                Title = "Changed Test Trip Title",
                Rating = 10,
                JourneyId = 1
            };

            tripService.Edit(tripId, tripToEdit);

            //Assert : description of trip is changed
            Assert.That(trip.Title, Is.EqualTo("Changed Test Trip Title"));
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
            Assert.That(trips.Count() == tripsCount);
        }


       
        [Test]
        public void Test_TripService_GetTripDetailsById()
        {
            //Arange
            int tripId = 1;

            //Act : get trip
            var trip = tripService.GetTripDetailsById(tripId).Result;

            //Assert : trip properties are correct
            Assert.That(trip.Title == "Test Trip Title");          
            Assert.That(trip.Rating == 10);
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
        public void Test_TripService_GetTripById()
        {
            //Arange
            int tripId = 1;

            //Act : get trip
            var trip = tripService.GetTripById(tripId).Result;

            //Assert : post properties are correct
            Assert.That(trip.Title == "Test Trip Title");
            Assert.That(trip.ApplicationUserId == "TestuserId");
            Assert.That(trip.Rating == 10);
            Assert.That(trip.JourneyId == 1);
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
        public void Test_TripService_GetTripsForSelect()
        {
            //Arange
            int tripsCount = data.Trips.Count();

            //Act : get all trips
            var trips = tripService.GetTripsForSelect().Result;

            //Assert : number of trips is correct
            Assert.That(trips.Count() == tripsCount);
        }


        [Test]
        public void Test_TripService_EditCreateForm()
        {
            //Arrange
            int tripId = 2;
            var trip = tripService.GetTripById(tripId).Result;

            //Act
            var tripEditForm = tripService.EditCreateForm(tripId).Result;

            //Assert
            Assert.That(trip.Title == tripEditForm.Title);
            Assert.That(trip.Rating == tripEditForm.Rating);

        }

        [Test]
        public void Test_TownService_DeleteCreateForm()
        {
            //Arrange
            int tripId = 3;
            var trip = tripService.GetTripById(tripId).Result;

            //Act
            var tripDeleteForm = tripService.DeleteCreateForm(tripId).Result;

            //Assert
            Assert.That(trip.Id == tripDeleteForm.Id);
            Assert.That(trip.Title == tripDeleteForm.Title);
            Assert.That(trip.Rating == tripDeleteForm.Rating);

        }
    }
}
