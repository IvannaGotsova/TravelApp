using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.JourneyModels;
using TravelApp.Data.Models.TripModels;

namespace TravelApp.Core.Contracts
{
    /// <summary>
    /// Holds Interface for Country functionality.
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// This method returns IEnumerable of all countries.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AllCountriesModel>> GetAllCountries();
        /// <summary>
        /// This method is used to add a country.
        /// </summary>
        /// <param name="addCountryModel"></param>
        /// <returns></returns>
        Task Add(AddCountryModel addCountryModel);
        /// <summary>
        /// This method returns IEnumerable of all countries used for Select.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Country>> GetCountriesForSelect();
        /// <summary>
        /// This method returns Details of particular country with given id.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        Task<DetailsCountryModel> GetCountryDetailsById(int countryId);
        /// <summary>
        /// This method creates form for editing a country.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        Task<EditCountryModel> EditCreateForm(int countryId);
        /// <summary>
        /// This method is used to edit a particular country with given id.
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="editCountryModel"></param>
        /// <returns></returns>
        Task Edit(int countryId, EditCountryModel editCountryModel);
        /// <summary>
        /// This method returns a particular country with given id.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        Task<Country> GetCountryById(int countryId);
        /// <summary>
        /// This method creates form for deleting a country.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        Task<DeleteCountryModel> DeleteCreateForm(int countryId);
        /// <summary>
        /// This method deletes a particular country with given id.
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        Task Delete(int countryId);

    }
}
