﻿using System;
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
        private IJourneyService journeyService;
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
            Assert.That(this.data.Countries.Count() == countriesCount + 1);
        }

        [Test]
        public void Test_CountryService_Edit()
        {
            //Arrange
            int countryId = 3;
            var country = this.data.Countries.Where(c => c.Id == countryId).First();

            //Act : edit a country
            var countryToEdit = new EditCountryModel()
            {
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
            Assert.That(countries.Count() == countriesCount);
        }



        [Test]
        public void Test_CountryService_GetCountriesForSelect()
        {
            //Arange
            int countriesCount = data.Countries.Count();

            //Act : get all countries
            var countries = countryService.GetCountriesForSelect().Result;

            //Assert : number of countries is correct
            Assert.That(countries.Count() == countriesCount);
        }


        [Test]
        public void Test_CountryService_GetCountryById()
        {
            //Arange
            int countryId = 1;

            //Act : get country
            var country = countryService.GetCountryById(countryId).Result;

            //Assert : country properties are correct
            Assert.That(country.Name == "Test Country Name");
            Assert.That(country.Description == "Test Country Description");
            Assert.That(country.Population == 6823493);
            Assert.That(country.Area == 110994);
            Assert.That(country.Image == "/Photos/Test");

        }
        [Test]
        public void Test_CountryService_GetCommentByIdNull()
        {
            //Arange
            int countryId = 10001;

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

            //Assert : country properties are correct
            Assert.That(country.Name == "Test Country Name");
            Assert.That(country.Description == "Test Country Description");
            Assert.That(country.Population == 6823493);
            Assert.That(country.Area == 110994);
            Assert.That(country.Image == "/Photos/Test");

        }
        [Test]
        public void Test_CountryService_GetCommentDetailsByIdNull()
        {

            //Arange
            int countryId = 10001;

            //Act 
            //Assert :throw ArgumentNullException
            Assert.ThrowsAsync<ArgumentNullException>(async () => await countryService.GetCountryDetailsById(countryId));
        }



    }
}