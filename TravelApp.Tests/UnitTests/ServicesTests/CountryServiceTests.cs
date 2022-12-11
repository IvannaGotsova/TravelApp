using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Tests.UnitTests.ServicesTests
{
    [TestFixture]
    public class CountryServiceTests : UnitTestsBase
    {
        private ICountryService countryService;
        private readonly IJourneyService journeyService;
        private IRepository repository;


        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);

            countryService = new CountryService(repository, journeyService);          
        }

        [Test]
        public void Test_CountryService_Add()
        {
            //Arrange
            int countriesCount = this.data.Countries.Count();

            //Act : create and add new country
            var countryToAdd = new AddCountryModel()
            {
                Name = "Test Country Name 4",
                Description = "Test Country Description",
                Population = 6823493,
                Image = "/Photos/Test",
                Area = 110994,
            };

            countryService.Add(countryToAdd);

            //Assert : new comment is added
            Assert.That(this.data.Countries.Count(), Is.EqualTo(countriesCount + 1));
        }

        [Test]
        public void Test_CountryService_Edit()
        {
            //Arrange
            int countryId = 2;
            var country = this.data.Countries.Where(c => c.Id == countryId).First();

            //Act : edit a country
            var countryToEdit = new EditCountryModel()
            {
                Id = 2,
                Name = "Test Country Name 4",
                Description = "Changed Test Country Description",
                Population = 6823493,
                Image = "/Photos/Test",
                Area = 110994,
            };

            countryService.Edit(countryId, countryToEdit);

            //Assert : description of country is changed
            Assert.That(country.Description, Is.EqualTo("Changed Test Country Description"));
        }

        [Test]
        public void Test_CountryService_Delete()
        {
            //Arrange
            int countryId = 3;
            int countriesCount = data.Countries.Count();

            //Act : deletes the country with given id
            countryService.Delete(countryId);

            //Assert : number of countries is correct
            Assert.That(data.Countries.Count(), Is.EqualTo(countriesCount - 1));
        }

        [Test]
        public void Test_CountryService_GetAllCountries()
        {
            //Arange
            int countriesCount = data.Countries.Count();

            //Act : get all countries
            var countries = countryService.GetAllCountries().Result;

            //Assert : number of countries is correct

            Assert.That(countries.Count(), Is.EqualTo(countriesCount));
        }



        [Test]
        public void Test_CountryService_GetCountriesForSelect()
        {
            //Arange
            int countriesCount = data.Countries.Count();

            //Act : get all countries
            var countries = countryService.GetCountriesForSelect().Result;

            //Assert : number of countries is correct
            Assert.That(countries.Count(), Is.EqualTo(countriesCount));
        }


        [Test]
        public void Test_CountryService_GetCountryById()
        {
            //Arange
            int countryId = 1;

            //Act : get country
            var country = countryService.GetCountryById(countryId).Result;

            Assert.Multiple(() =>
            {

                //Assert : country properties are correct
                Assert.That(country.Name, Is.EqualTo("Test Country Name"));
                Assert.That(country.Description, Is.EqualTo("Test Country Description"));
                Assert.That(country.Population, Is.EqualTo(6823493));
                Assert.That(country.Area, Is.EqualTo(110994));
                Assert.That(country.Image, Is.EqualTo("/Photos/Test"));
            });
        }

        [Test]
        public void Test_CountryService_GetCountryByIdNull()
        {
            //Arange
            int countryId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await countryService.GetCountryById(countryId));
        }

        [Test]
        public void Test_CountryService_GetCountryByIdNull_2()
        {
            //Arange
            int countryId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await countryService.GetCountryById(countryId));
        }

        [Test]
        public void Test_CountryService_GetCountryDetailsById()
        {
            //Arange
            int countryId = 1;

            //Act : get country
            var country = countryService.GetCountryDetailsById(countryId).Result;

            Assert.Multiple(() =>
            {

                //Assert : country properties are correct
                Assert.That(country.Name, Is.EqualTo("Test Country Name"));
                Assert.That(country.Description, Is.EqualTo("Test Country Description"));
                Assert.That(country.Population, Is.EqualTo(6823493));
                Assert.That(country.Area, Is.EqualTo(110994));
                Assert.That(country.Image, Is.EqualTo("/Photos/Test"));
            });
        }

        [Test]
        public void Test_CountryService_GetCountryDetailsByIdNull()
        {

            //Arange
            int countryId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await countryService.GetCountryDetailsById(countryId));
        }

        [Test]
        public void Test_CountryService_GetCountryDetailsByIdNull_2()
        {

            //Arange
            int countryId = -56;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await countryService.GetCountryDetailsById(countryId));
        }


        [Test]
        public void Test_CountryService_EditCreateForm()
        {
            //Arrange
            int countryId = 2;
            var country = countryService.GetCountryById(countryId).Result;

            //Act
            var countryEditForm = countryService.EditCreateForm(countryId).Result;

            Assert.Multiple(() =>
            {

                //Assert
                Assert.That(country.Name, Is.EqualTo(countryEditForm.Name));
                Assert.That(country.Description, Is.EqualTo(countryEditForm.Description));
                Assert.That(country.Population, Is.EqualTo(countryEditForm.Population));
                Assert.That(country.Area, Is.EqualTo(countryEditForm.Area));
                Assert.That(country.Image, Is.EqualTo(countryEditForm.Image));
            });
        }

        [Test]
        public void Test_CountryService_DeleteCreateForm()
        {
            //Arrange
            int countryId = 4;
            var country = countryService.GetCountryById(countryId).Result;

            //Act
            var countryDeleteForm = countryService.DeleteCreateForm(countryId).Result;

            Assert.Multiple(() =>
            {

                //Assert
                Assert.That(country.Id, Is.EqualTo(countryDeleteForm.Id));
                Assert.That(country.Name, Is.EqualTo(countryDeleteForm.Name));
                Assert.That(country.Description, Is.EqualTo(countryDeleteForm.Description));
            });
        }
    }
}
