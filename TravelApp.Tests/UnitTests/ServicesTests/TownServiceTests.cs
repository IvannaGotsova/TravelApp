using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.TownModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class TownServiceTests : UnitTestsBase
    {
        private ITownService townService;
        private IRepository repository;


        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);

            townService = new TownService(repository);          
        }

        [Test]
        public void Test_TownService_Add()
        {
            //Arrange
            int townsCount = this.data.Towns.Count();

            //Act : create and add new town
            var townToAdd = new AddTownModel()
            {
                Name = "Test Town Name 4",
                Population = 8930002,
                Area = 77818,
                Description = "Test Town Description",
                CountryId = 1,
                Image = "/Photos/Test"
            };

            townService.Add(townToAdd);
            //Assert : new town is added
            Assert.That(this.data.Towns.Count() == townsCount + 1);
        }

        [Test]
        public void Test_TownService_Edit()
        {
            //Arrange
            int townId = 2;
            var town = this.data.Towns.Where(t => t.Id == townId).First();

            //Act : edit a town
            var townToEdit = new EditTownModel()
            {
                Name = "Test Town Name 4",
                Population = 8930002,
                Area = 77818,
                Description = "Changed Test Town Description",
                CountryId = 1,
                Image = "/Photos/Test"
            };

            townService.Edit(townId, townToEdit);

            //Assert : description of town is changed
            Assert.That(town.Description, Is.EqualTo("Changed Test Town Description"));
        }

        [Test]
        public void Test_TownService_Delete()
        {
            //Arrange
            int townId = 3;
            int townsCount = data.Towns.Count();

            //Act : deletes the town with given id
            townService.Delete(townId);

            //Assert : number of towns is correct
            Assert.That(data.Towns.Count(), Is.EqualTo(townsCount - 1));
        }

        [Test]
        public void Test_TownsService_GetTowns()
        {
            //Arange
            int townsCount = data.Towns.Count();

            //Act : get all towns
            var towns = townService.GetAllTowns().Result;

            //Assert : number of towns is correct
            Assert.That(towns.Count() == townsCount);
        }

        [Test]
        public void Test_TownService_GetTownsForSelect()
        {
            //Arange
            int townsCount = data.Towns.Count();

            //Act : get all towns
            var towns = townService.GetTownsForSelect().Result;

            //Assert : number of towns is correct
            Assert.That(towns.Count() == townsCount);
        }


        [Test]
        public void Test_TownService_GetTownById()
        {
            //Arange
            int townId = 1;

            //Act : get town
            var town = townService.GetTownById(townId).Result;

            //Assert : town properties are correct
            Assert.That(town.Name == "Test Town Name");
            Assert.That(town.Description == "Test Town Description");
            Assert.That(town.Population == 8930002);
            Assert.That(town.Area == 77818);
            Assert.That(town.Image == "/Photos/Test");
            Assert.That(town.CountryId == 1);

        }

        [Test]
        public void Test_TownService_GetTownByIdNull()
        {
            //Arange
            int townId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await townService.GetTownById(townId));
        }


        [Test]
        public void Test_TownService_GetTownByIdNull_2()
        {
            //Arange
            int townId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await townService.GetTownById(townId));
        }

        [Test]
        public void Test_CountryService_GetCountryDetailsById()
        {
            //Arange
            int townId = 1;

            //Act : get town
            var town = townService.GetTownDetailsById(townId).Result;

            //Assert : town properties are correct
            Assert.That(town.Name == "Test Town Name");
            Assert.That(town.Description == "Test Town Description");
            Assert.That(town.Population == 8930002);
            Assert.That(town.Area == 77818);
            Assert.That(town.Image == "/Photos/Test");
        }

        [Test]
        public void Test_TownService_GetTownDetailsByIdNull()
        {
            //Arange
            int townId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await townService.GetTownDetailsById(townId));
        }

        [Test]
        public void Test_TownService_GetTownDetailsByIdNull_2()
        {
            //Arange
            int townId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await townService.GetTownDetailsById(townId));
        }

        [Test]
        public void Test_TownService_EditCreateForm()
        {
            //Arrange
            int townId = 2;
            var town = townService.GetTownById(townId).Result;

            //Act
            var townEditForm = townService.EditCreateForm(townId).Result;

            //Assert
            Assert.That(town.Name == townEditForm.Name);
            Assert.That(town.Description == townEditForm.Description);
            Assert.That(town.Population == townEditForm.Population);
            Assert.That(town.Area == townEditForm.Area);
            Assert.That(town.Image == townEditForm.Image);

        }

        [Test]
        public void Test_TownService_DeleteCreateForm()
        {
            //Arrange
            int townId = 4;
            var town = townService.GetTownById(townId).Result;

            //Act
            var townDeleteForm = townService.DeleteCreateForm(townId).Result;

            //Assert
            Assert.That(town.Name == townDeleteForm.Name);
            Assert.That(town.Description == townDeleteForm.Description);

        }
    }
}
