using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.JourneyModels;
using TravelApp.Data.Models.TownModels;

namespace TravelApp.Core.Contracts
{
    /// <summary>
    /// Holds Interface for Town functionality.
    /// </summary>
    public interface ITownService
    {
        /// <summary>
        /// This method returns IEnumerable of all towns.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AllTownsModel>> GetAllTowns();
        /// <summary>
        /// This method is used to add a town.
        /// </summary>
        /// <param name="addTownModel"></param>
        /// <returns></returns>
        Task Add(AddTownModel addTownModel);
        /// <summary>
        /// This method returns IEnumerable of all Towns used for Select.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Town>> GetTownsForSelect();
        /// <summary>
        /// This method returns Details of particular town with given id.
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        Task<DetailsTownModel> GetTownDetailsById(int townId);
        /// <summary>
        /// This method creates form for editing a town.
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        Task<EditTownModel> EditCreateForm(int townId);
        /// <summary>
        /// This method is used to edit a particular town with given id.
        /// </summary>
        /// <param name="townId"></param>
        /// <param name="editTownModel"></param>
        /// <returns></returns>
        Task Edit(int townId, EditTownModel editTownModel);
        /// <summary>
        /// This method returns a particular town with given id.
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        Task<Town> GetTownById(int townId);
        /// <summary>
        /// This method creates form for deleting a town.
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        Task<DeleteTownModel> DeleteCreateForm(int townId);
        /// <summary>
        /// This method deletes a particular town with given id.
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        Task Delete(int townId);

    }
}
