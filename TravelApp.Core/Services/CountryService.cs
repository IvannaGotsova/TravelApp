using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.JourneyModels;
using TravelApp.Data.Models.TripModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Core.Services
{
    /// <summary>
    /// Holds Country functionality.
    /// </summary>
    public class CountryService : ICountryService
    {
        private readonly IRepository data;
        private readonly IJourneyService journeyService;

        public CountryService(IRepository data,
                              IJourneyService journeyService)
        {
            this.data = data;
            this.journeyService = journeyService;
        }
        /// <summary>
        /// This method is used to add a country.
        /// </summary>
        /// <param name="addCountryModel"></param>
        /// <returns></returns>
        public async Task Add(AddCountryModel addCountryModel)
        {          
            var countryToBeAddes = new Country()
            {
                Name = addCountryModel.Name,
                Population = addCountryModel.Population,
                Area = addCountryModel.Area,
                Description = addCountryModel.Description,
                Image = addCountryModel.Image,
            };

            await this.data.AddAsync(countryToBeAddes);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method deletes a particular country with given id.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public async Task Delete(int countryId)
        {
            await this.data.DeleteAsync<Country>(countryId);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method creates form for deleting a country.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public async Task<DeleteCountryModel> DeleteCreateForm(int countryId)
        {
            var countryToBeDeleted = await
              GetCountryById(countryId);

            var deleteCountryModel = new DeleteCountryModel()
            {
                Id = countryToBeDeleted.Id,
                Name= countryToBeDeleted.Name,
                Description = countryToBeDeleted.Description
            };

            return deleteCountryModel;
        }
        /// <summary>
        /// This method is used to edit a particular country with given id.
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="editCountryModel"></param>
        /// <returns></returns>
        public async Task Edit(int countryId, EditCountryModel editCountryModel)
        {
            var countryToBeEdited = await
                     GetCountryById(countryId);

            countryToBeEdited!.Name = editCountryModel.Name;
            countryToBeEdited.Description = editCountryModel.Description;
            countryToBeEdited.Population = editCountryModel.Population;
            countryToBeEdited.Area = editCountryModel.Area;
            countryToBeEdited.Image = editCountryModel.Image;

            this.data.Update(countryToBeEdited);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method creates form for editing a country.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public async Task<EditCountryModel> EditCreateForm(int countryId)
        {
            var countryToBeEdited = await
               GetCountryById(countryId);

            var editCountryModel = new EditCountryModel()
            {
                Name = countryToBeEdited.Name,
                Description = countryToBeEdited.Description,
                Population = countryToBeEdited.Population,
                Area = countryToBeEdited.Area,
                Image = countryToBeEdited.Image,
            };

            return editCountryModel;
        }
        /// <summary>
        /// This method returns IEnumerable of all countries.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AllCountriesModel>> GetAllCountries()
        {
            var countries = await data
                 .AllReadonly<Country>()
                 .ToListAsync();

            return countries
                .Select(c => new AllCountriesModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Population = c.Population,
                    Area = c.Area,
                    Image = c.Image,
                    Description = c.Description,
                })
                .ToList();
        }
        /// <summary>
        /// This method returns IEnumerable of all countries used for Select.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Country>> GetCountriesForSelect()
        {
            return await
                this.data
                .AllReadonly<Country>()
                .ToListAsync();

        }
        /// <summary>
        /// This method returns a particular country with given id.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public async Task<Country> GetCountryById(int countryId)
        {
            var country = await
                     this.data
                     .AllReadonly<Country>()
                     .Where(c => c.Id == countryId)
                     .FirstOrDefaultAsync();
            //check if country is null
            if (country == null)
            {
                throw new ArgumentNullException();
            }

            return country;
        }
        /// <summary>
        /// This method returns Details of particular country with given id.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public async Task<DetailsCountryModel> GetCountryDetailsById(int countryId)
        {
           var country = await
               this.data
               .AllReadonly<Country>()
               .Where(c => c.Id == countryId)
               .Select(c => new DetailsCountryModel()
               {
                   Id = c.Id,
                   Name = c.Name,
                   Description = c.Description,
                   Population = c.Population,
                   Area = c.Area,
                   Image = c.Image,
                   JourneysForCountry = c.CountriesJourneys.Where(cj => cj.CountryId == countryId)
                                                           .Select(cj => new DetailsJourneyModel()
                                                           {
                                                               Id = cj.Journey!.Id,
                                                               Image = cj.Journey.Image,
                                                               Title = cj.Journey.Title,
                                                               Days = cj.Journey.Days
                                                           }).ToList()
               }).FirstOrDefaultAsync();

            //check if country is null
            if (country == null)
            {
                throw new ArgumentNullException();
            }

            return country;
        }
    }
}
