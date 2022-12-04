﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.JourneyModels;
using TravelApp.Data.Models.PostModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Core.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IRepository data;
        

        public JourneyService(IRepository data)
        {
            this.data = data;         
        }

        public async Task Add(AddJourneyModel addJourneyModel, string currentUserId)
        {
            var journeyToBeAdded = new Journey()
            {
                Title = addJourneyModel.Title,
                Description = addJourneyModel.Description,
                StartDate = addJourneyModel.StartDate,
                EndDate = addJourneyModel.EndDate,
                Image = addJourneyModel.Image,
                Days = addJourneyModel.Days,
                Price = addJourneyModel.Price,
                NumberOfPeople = addJourneyModel.NumberOfPeople,
                CountriesJourneys = new List<CountryJourney>(),
                ApplicationUsersJourneys = new List<ApplicationUserJourney>(),
                TownsJourneys = new List<TownJourney>()
                
            };

            await this.data.AddAsync(journeyToBeAdded);

            foreach (var country in addJourneyModel.CountryIds)
            {
                var journeyCountryToAdded = new CountryJourney()
                {
                    CountryId = country,
                    Journey = journeyToBeAdded,

                };

                await this.data.AddAsync(journeyCountryToAdded);
            }

            foreach (var town in addJourneyModel.TownIds)
            {
                var journeyTownToAdded = new TownJourney()
                {
                    TownId = town,
                    Journey = journeyToBeAdded,

                };

                await this.data.AddAsync(journeyTownToAdded);
            }

            var journeyUserToBeAdded = new ApplicationUserJourney()
            {
                ApplicationUserId = currentUserId,
                Journey = journeyToBeAdded,
            };


            await this.data.AddAsync(journeyUserToBeAdded);
            await this.data.SaveChangesAsync();
        }

        public async Task Delete(int journeyId, string currentUserId)
        {
            //var countriesJourneys = await
            //    this.data
            //    .AllReadonly<CountryJourney>()
            //    .ToListAsync();

            //var applicationUsersJourneys = await
            //    this.data
            //    .AllReadonly<ApplicationUserJourney>()
            //    .ToListAsync();

            //foreach (var item in countriesJourneys.Where(cj => cj.JourneyId == journeyId))
            //{
            //    var countryJourney = new CountryJourney()
            //    {
            //        JourneyId = journeyId,
            //        CountryId = item.CountryId
            //    };

            //    await this.data.DeleteAsync<CountryJourney>(countryJourney);
            //}

            //foreach (var item in applicationUsersJourneys.Where(cj => cj.JourneyId == journeyId))
            //{
            //    var applicationUserJourney = new ApplicationUserJourney()
            //    {
            //        JourneyId = journeyId,
            //        ApplicationUserId = currentUserId
            //    };

            //    await this.data.DeleteAsync<ApplicationUser>(applicationUserJourney);
            //}

            await this.data.DeleteAsync<Journey>(journeyId);
            await this.data.SaveChangesAsync();
        }

        public async Task<DeleteJourneyModel> DeleteCreateForm(int journeyId)
        {
            var journeyToBeDeleted = await
               GetJourneyById(journeyId);

            var deleteJourneyModel = new DeleteJourneyModel()
            {
                Id = journeyToBeDeleted.Id,
                Title = journeyToBeDeleted.Title,
                Description = journeyToBeDeleted.Description,
            };

            return deleteJourneyModel;
        }

        public async Task Edit(int journeyId, EditJourneyModel editJourneyModel)
        {
            var journeyToBeEdited = await
                     GetJourneyById(journeyId);

            journeyToBeEdited.Title = editJourneyModel.Title;
            journeyToBeEdited.Image = editJourneyModel.Image;
            journeyToBeEdited.Description = editJourneyModel.Description;
            journeyToBeEdited.StartDate = editJourneyModel.StartDate;
            journeyToBeEdited.EndDate = editJourneyModel.EndDate;
            journeyToBeEdited.Price = editJourneyModel.Price;
            journeyToBeEdited.Days = editJourneyModel.Days;
            journeyToBeEdited.NumberOfPeople = editJourneyModel.NumberOfPeople;

            this.data.Update(journeyToBeEdited);

            var countryJourneysToDlete = await
                this.data
                .AllReadonly<CountryJourney>()
                .Where(cj => cj.JourneyId == journeyId)
                .ToListAsync();

            this.data.DeleteRange<CountryJourney>(countryJourneysToDlete);

            foreach(var countryJourney in editJourneyModel.CountryIds)
            {
                var journeyCountryToAdded = new CountryJourney()
                {
                    CountryId = countryJourney, 
                    Journey = journeyToBeEdited,

                };

                await this.data.AddAsync(journeyCountryToAdded);
            }

            var townsJourneysToDelete = await
                this.data
                .AllReadonly<TownJourney>()
                .Where(tj => tj.JourneyId == journeyId)
                .ToListAsync();

            this.data.DeleteRange<TownJourney>(townsJourneysToDelete);

            foreach (var townJourney in editJourneyModel.TownIds)
            {
                var journeyTownToAdded = new TownJourney()
                {
                    TownId = townJourney,
                    Journey = journeyToBeEdited,

                };

                await this.data.AddAsync(journeyTownToAdded);
            }
            await this.data.SaveChangesAsync();
        }

        public async Task<EditJourneyModel> EditCreateForm(int journeyId)
        {
            var journeyToBeEdited = await
               GetJourneyById(journeyId);

            var editJourneyModel = new EditJourneyModel()
            {
                Title = journeyToBeEdited.Title,
                Description = journeyToBeEdited.Description,
                StartDate = journeyToBeEdited.StartDate,
                EndDate = journeyToBeEdited.EndDate,
                Days = journeyToBeEdited.Days,
                Price = journeyToBeEdited.Price,
                NumberOfPeople = journeyToBeEdited.NumberOfPeople,
                Image = journeyToBeEdited.Image,
                Countries = new List<Country>(),
                Towns = new List<Town>()
            };

            return editJourneyModel;
        }

        public async Task<IEnumerable<AllJourneysModel>> GetAllJourneys()
        {
            var journeys = await 
                  this.data
                 .AllReadonly<Journey>()
                 .Include(j => j.CountriesJourneys)
                 .ThenInclude(cj => cj.Country)
                 .ThenInclude(c => c!.Towns)
                 .ToListAsync();



            return journeys
                 .Select(j => new AllJourneysModel()
                 {
                     Id = j.Id,
                     Title = j.Title,
                     Description = j.Description,
                     Image = j.Image,
                     Countries = string.Join(", ", j.CountriesJourneys.Select(cj => cj.Country!.Name)),
                 })
                .ToList();
        }

        public async Task<Journey> GetJourneyById(int journeyId)
        {
            var journey = await
              this.data
              .AllReadonly<Journey>()
              .Where(j => j.Id == journeyId)
              .FirstOrDefaultAsync();

            if (journey == null)
            {
                throw new ArgumentNullException();
            }

            return journey;
        }

        public async Task<DetailsJourneyModel> GetJourneyDetailsById(int journeyId)
        {         
            var journey = await
                this.data
                .AllReadonly<Journey>()
                .Include(j => j.CountriesJourneys
                .Where(cj => cj.JourneyId == journeyId))
                .ThenInclude(cj => cj.Country)
                .Include(j => j.TownsJourneys
                .Where(tj => tj.JourneyId == journeyId))
                .ThenInclude(tj => tj.Town)
                .Select(j => new DetailsJourneyModel()
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    StartDate = j.StartDate,
                    EndDate = j.EndDate,
                    Price = j.Price,
                    Days = j.Days,
                    NumberOfPeople = j.NumberOfPeople,
                    Image = j.Image,
                    Countries = string.Join(", ", j.CountriesJourneys.Select(cj => cj.Country!.Name)),
                    Towns = string.Join(", ", j.TownsJourneys.Select(tj => tj.Town!.Name))
                }).FirstOrDefaultAsync();           


            if (journey == null)
            {
                throw new ArgumentNullException();
            }

            return journey;
        }

       
        public async Task<IEnumerable<Journey>> GetJourneysForSelect()
        {
            return await
                this.data
                .AllReadonly<Journey>()
                .ToListAsync();
        }

    }
}