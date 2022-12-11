using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.JourneyModels;
using TravelApp.Data.Models.TownModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Core.Services
{
    /// <summary>
    /// Holds Town functionality.
    /// </summary>
    public class TownService : ITownService
    {
        private readonly IRepository data;

        public TownService(IRepository data)
        {
            this.data = data;
        }
        /// <summary>
        /// This method is used to add a town.
        /// </summary>
        /// <param name="addTownModel"></param>
        /// <returns></returns>
        public async Task Add(AddTownModel addTownModel)
        {
            var townToBeAdded = new Town()
            {
                Name = addTownModel.Name,
                Population = addTownModel.Population,
                Area = addTownModel.Area,
                Description = addTownModel.Description,
                Image = addTownModel.Image,
                CountryId = addTownModel.CountryId
            };

            await this.data.AddAsync(townToBeAdded);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method deletes a particular town with given id.
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        public async Task Delete(int townId)
        {
            await this.data.DeleteAsync<Town>(townId);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method creates form for deleting a town.
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        public async Task<DeleteTownModel> DeleteCreateForm(int townId)
        {
            var townToBeDeleted = await
               GetTownById(townId);

            var deleteTownModel = new DeleteTownModel()
            {
                Id = townToBeDeleted.Id,
                Name = townToBeDeleted.Name,
                Description = townToBeDeleted.Description,
            };

            return deleteTownModel;
        }
        /// <summary>
        /// This method is used to edit a particular town with given id.
        /// </summary>
        /// <param name="townId"></param>
        /// <param name="editTownModel"></param>
        /// <returns></returns>
        public async Task Edit(int townId, EditTownModel editTownModel)
        {
            var townToBeEdited = await
                     GetTownById(townId);

            townToBeEdited.Name = editTownModel.Name;
            townToBeEdited.Image = editTownModel.Image;
            townToBeEdited.Description = editTownModel.Description;
            townToBeEdited.Population = editTownModel.Population;
            townToBeEdited.Area = editTownModel.Area;

            this.data.Update<Town>(townToBeEdited);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method creates form for editing a town.
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        public async Task<EditTownModel> EditCreateForm(int townId)
        {
            var townToBeEdited = await
              GetTownById(townId);


            var editJourneyModel = new EditTownModel()
            {
                Name = townToBeEdited.Name,
                Description = townToBeEdited.Description,
                Population = townToBeEdited.Population,
                Area = townToBeEdited.Area,
                Image = townToBeEdited.Image,
                Countries = new List<Country>(),

            };

            return editJourneyModel;
        }
        /// <summary>
        /// This method returns IEnumerable of all towns.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AllTownsModel>> GetAllTowns()
        {
            var towns = await data
                 .AllReadonly<Town>()
                 .Include(t => t.Country)
                 .ToListAsync();

            return towns
                .Select(t => new AllTownsModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Population = t.Population,
                    Area = t.Area,
                    Image = t.Image,
                    CountryName = t.Country!.Name,
                    Description = t.Description,
                    CountryId = t.CountryId
                })
                .ToList();
        }
        /// <summary>
        /// This method returns a particular town with given id.
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        public async Task<Town> GetTownById(int townId)
        {
            var town = await
              this.data
              .AllReadonly<Town>()
              .Where(t => t.Id == townId)
              .FirstOrDefaultAsync();
            //check if town is null
            if (town == null)
            {
                throw new ArgumentNullException(null, nameof(town));
            }

            return town;
        }
        /// <summary>
        /// This method returns Details of particular town with given id.
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        public async Task<DetailsTownModel> GetTownDetailsById(int townId)
        {

            var town = await
               this.data
              .AllReadonly<Town>()
              .Where(t => t.Id == townId)
              .Select(t => new DetailsTownModel()
              {
                  Id = t.Id,
                  Name = t.Name,
                  Description = t.Description,
                  Population = t.Population,
                  Area = t.Area,
                  Image = t.Image,
                  JourneysForTown = t.TownsJourneys.Where(tj => tj.TownId == townId)
                                                           .Select(tj => new DetailsJourneyModel()
                                                           {
                                                               Id = tj.Journey!.Id,
                                                               Image = tj.Journey.Image,
                                                               Title = tj.Journey.Title,
                                                               Days = tj.Journey.Days
                                                           }).ToList()
              }).FirstOrDefaultAsync();
            //check if town is null
            if (town == null)
            {
                throw new ArgumentNullException(null, nameof(town));
            }

            return town;
        }
        /// <summary>
        /// This method returns IEnumerable of all Towns used for Select.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Town>> GetTownsForSelect()
        {
            return await
                this.data
                .AllReadonly<Town>()
                .ToListAsync();
        }
    }
}
