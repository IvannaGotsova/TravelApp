using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data;
using TravelApp.Data.Entities;
using TravelApp.Tests.Mocks;

namespace TravelApp.Tests.UnitTests
{
    public class UnitTestsBase
    {
        protected TravelAppDbContext data;

        [OneTimeSetUp]
        public void SetUpBase()
        {
            this.data = DatabaseMock.Instance;
            this.SeedDatabase();
        }
        public ApplicationUser ApplicationUser { get; private set; } = null!;
        public ApplicationUser ApplicationUserDelete { get; private set; } = null!;
        public ApplicationUser ApplicationUserVip { get; private set; } = null!;
        public ApplicationUser ApplicationUserNonVip { get; private set; } = null!;
        public Comment Comment { get; private set; } = null!;
        public Country Country { get; private set; } = null!;
        public Journey Journey { get; private set; } = null!;
        public Post  Post { get; private set; } = null!;
        public Town Town { get; private set; } = null!;
        public Trip Trip { get; private set; } = null!;
        public Request Request { get; private set; } = null!;
        public ApplicationUserJourney ApplicationUserJourney { get; private set; } = null!;
        public CountryJourney CountryJourney{ get; private set; } = null!;
        public TownJourney TownJourney { get; private set; } = null!;
        public ApplicationRole ApplicationRole { get; private set; } = null!;
        private void SeedDatabase()
        {
            this.ApplicationUser = new ApplicationUser()
            {
                Id = "TestuserId",
                Email = "test@test.com",
                UserName = "test@test.com",
                FirstName = "TestUserFirstName",
                LastName = "TestUserLastName",
                IsVIP = true
            };

            this.data.Add(ApplicationUser);

            this.ApplicationUserDelete = new ApplicationUser()
            {
                Id = "ApplicationUserDelete",
                Email = "test2@test.com",
                UserName = "test2@test.com",
                FirstName = "TestUserFirstName",
                LastName = "TestUserLastName",
                IsVIP = true
            };

            this.data.Add(ApplicationUserDelete);

            this.ApplicationUserVip = new ApplicationUser()
            {
                Id = "TestuserIdVip",
                Email = "test3@test.com",
                UserName = "test3@test.com",
                FirstName = "TestUserFirstName",
                LastName = "TestUserLastName",
                IsVIP = true
            };
            this.data.Users.Add(ApplicationUserVip);

            this.ApplicationUserNonVip = new ApplicationUser()
            {
                Id = "TestuserIdNonVip",
                Email = "test4@test.com",
                UserName = "test4@test.com",
                FirstName = "TestUserFirstName",
                LastName = "TestUserLastName",
                IsVIP = false
            };
            this.data.Users.Add(ApplicationUserNonVip);

            this.Country = new Country()
            {
                Id = 1,
                Name = "Test Country Name",
                Description = "Test Country Description",
                Population = 6823493,
                Image = "/Photos/Test",
                Area = 110994,              
            };

            this.data.Countries.Add(Country);

            this.Town = new Town()
            {
                Id = 1,
                Name = "Test Town Name",
                Population = 8930002,
                Area = 77818,
                Description = "Test Town Description",
                CountryId = 1,
                Image = "/Photos/Test"
            };

            this.data.Add(Town);

            this.Journey = new Journey()
            {
                Id = 1,
                Title = "Test Journey Title",
                Description = "Test Journey Description",
                StartDate = DateTime.ParseExact("01/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("11/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Price = 1000,
                NumberOfPeople = 3,
                Days = 5,
                Image = "/Photos/Test"
            };

            this.data.Journeys.Add(Journey);

            this.Trip = new Trip()
            {
                Id = 1,
                Title = "Test Trip Title",
                ApplicationUserId = "TestuserId",
                Rating = 10,
                JourneyId = 1
            };

            this.data.Trips.Add(Trip);

            this.Post = new Post()
            {
                Id = 1,
                Title = "Test Post Title",
                Description = "Test Post Description",
                TripId = 1,
                Image = "/Photos/Test"
            };
            this.data.Posts.Add(Post);

            
            this.Comment = new Comment()
            {
                Id = 1,
                Title = "Test Comment Title",
                Description = "Test Comment Description",
                PostId = 1,
                Author = "test@test.com",               
            };

            this.data.Comments.Add(Comment);

            this.Request = new Request()
            {
                Id = 1,
                NumberOfPeople = 2,
                JourneyId = 1,
                ApplicationUserId = "TestuserId",
                IsApproved = false,
                IsManaged = false
            };

            this.data.Requests.Add(Request);

            this.data.SaveChanges();
        }



        [OneTimeTearDown]

        public void TearDownBase()
            => this.data.Dispose();

    }
}
