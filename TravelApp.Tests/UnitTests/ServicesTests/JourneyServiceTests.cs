using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.JourneyModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class JourneyServiceTests : UnitTestsBase
    {
        private IJourneyService journeyService;
        private IRepository repository;


        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);

            journeyService = new JourneyService(repository);
        }




        [Test]
        public void Test_JourneyService_Add()
        {
            //Arrange
            int journeysCount = this.data.Journeys.Count();
            string currentUserId = "TestuserId";

            //Act : create and add new journey
            var journeyToAdd = new AddJourneyModel()
            {
                Title = "Test Journey Title",
                Description = "Test Journey Description",
                StartDate = DateTime.ParseExact("01/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("11/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Price = 1000,
                NumberOfPeople = 3,
                Days = 5,
                Image = "/Photos/Test"
            };

            journeyService.Add(journeyToAdd, currentUserId);
            //Assert : new journey is added
            Assert.That(this.data.Journeys.Count(), Is.EqualTo(journeysCount + 1));
        }

        [Test]
        public void Test_JourneyService_Delete()
        {
            //Arrange
            int journeyId = 3;
            string currentUserId = "TestuserId";
            int journeysCount = data.Journeys.Count();

            //Act : deletes the journey with given id
            journeyService.Delete(journeyId, currentUserId);

            //Assert : number of journeys is correct
            Assert.That(data.Journeys.Count(), Is.EqualTo(journeysCount - 1));
        }

       

        [Test]
        public void Test_JourneyService_GetAllJourneys()
        {
            //Arange
            int journeysCount = data.Journeys.Count();

            //Act : get all journeys
            var journeys = journeyService.GetAllJourneys().Result;

            //Assert : number of journeys is correct
            Assert.That(journeys.Count(), Is.EqualTo(journeysCount));
        }



        [Test]
        public void Test_JourneyService_GetJourneysForSelect()
        {
            //Arange
            int journeysCount = data.Journeys.Count();

            //Act : get all journeys
            var journeys = journeyService.GetJourneysForSelect().Result;

            //Assert : number of journeys is correct
            Assert.That(journeys.Count(), Is.EqualTo(journeysCount));
        }


        [Test]
        public void Test_JourneyService_GetJourneyById()
        {
            //Arange
            int journeyId = 1;

            //Act : get journey
            var journey = journeyService.GetJourneyById(journeyId).Result;

            Assert.Multiple(() =>
            {

                //Assert : journey properties are correct
                Assert.That(journey.Title, Is.EqualTo("Test Journey Title"));
                Assert.That(journey.Description, Is.EqualTo("Test Journey Description"));
                Assert.That(journey.StartDate, Is.EqualTo(DateTime.ParseExact("01/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture)));
                Assert.That(journey.EndDate, Is.EqualTo(DateTime.ParseExact("11/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture)));
                Assert.That(journey.Price, Is.EqualTo(1000));
                Assert.That(journey.NumberOfPeople, Is.EqualTo(3));
                Assert.That(journey.Days, Is.EqualTo(5));
                Assert.That(journey.Image, Is.EqualTo("/Photos/Test"));
            });
        }

        [Test]
        public void Test_JourneyService_GetCommentByIdNull()
        {
            //Arange
            int journeyId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await journeyService.GetJourneyById(journeyId));
        }


        [Test]
        public void Test_JourneyService_GetCommentByIdNull_2()
        {
            //Arange
            int journeyId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await journeyService.GetJourneyById(journeyId));
        }


        [Test]
        public void Test_JourneyService_GetCountryDetailsById()
        {
            //Arange
            int journeyId = 1;

            //Act : get journey
            var journey = journeyService.GetJourneyDetailsById(journeyId).Result;

            Assert.Multiple(() =>
            {

                //Assert : journey properties are correct
                Assert.That(journey.Title, Is.EqualTo("Test Journey Title"));
                Assert.That(journey.Description, Is.EqualTo("Test Journey Description"));
                Assert.That(journey.StartDate, Is.EqualTo(DateTime.ParseExact("01/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture)));
                Assert.That(journey.EndDate, Is.EqualTo(DateTime.ParseExact("11/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture)));
                Assert.That(journey.Price, Is.EqualTo(1000));
                Assert.That(journey.NumberOfPeople, Is.EqualTo(3));
                Assert.That(journey.Days, Is.EqualTo(5));
                Assert.That(journey.Image, Is.EqualTo("/Photos/Test"));
            });
        }

        [Test]
        public void Test_ApplicationUserService_GetCommentDetailsByIdNull()
        {

            int journeyId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await journeyService.GetJourneyDetailsById(journeyId));
        }

        [Test]
        public void Test_ApplicationUserService_GetCommentDetailsByIdNull_2()
        {

            int journeyId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await journeyService.GetJourneyDetailsById(journeyId));
        }


        [Test]
        public void Test_JourneyService_EditCreateForm()
        {
            //Arrange
            int journeyId = 2;
            var journey = journeyService.GetJourneyById(journeyId).Result;

            //Act
            var journeyEditForm = journeyService.EditCreateForm(journeyId).Result;

            Assert.Multiple(() =>
            {

                //Assert
                Assert.That(journey.Title, Is.EqualTo(journeyEditForm.Title));
                Assert.That(journey.Description, Is.EqualTo(journeyEditForm.Description));
                Assert.That(journey.StartDate, Is.EqualTo(journeyEditForm.StartDate));
                Assert.That(journey.EndDate, Is.EqualTo(journeyEditForm.EndDate));
                Assert.That(journey.Price, Is.EqualTo(journeyEditForm.Price));
                Assert.That(journey.NumberOfPeople, Is.EqualTo(journeyEditForm.NumberOfPeople));
                Assert.That(journey.Days, Is.EqualTo(journeyEditForm.Days));
                Assert.That(journey.Image, Is.EqualTo(journeyEditForm.Image));
            });
        }

        [Test]
        public void Test_JourneyService_DeleteCreateForm()
        {
            //Arrange
            int journeyId = 4;
            var journey = journeyService.GetJourneyById(journeyId).Result;

            //Act
            var journeyDeleteForm = journeyService.DeleteCreateForm(journeyId).Result;

            Assert.Multiple(() =>
            {

                //Assertm
                Assert.That(journey.Title, Is.EqualTo(journeyDeleteForm.Title));
                Assert.That(journey.Description, Is.EqualTo(journeyDeleteForm.Description));
            });
        }
    }
}
