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
    public interface ICountryService
    {
        Task<IEnumerable<AllCountriesModel>> GetAllCountries();
        Task Add(AddCountryModel addCountryModel);
        Task<IEnumerable<Country>> GetCountriesForSelect();
        Task<DetailsCountryModel> GetCountryDetailsById(int countryId);
        Task<EditCountryModel> EditCreateForm(int countryId);
        Task Edit(int countryId, EditCountryModel editCountryModel);
        Task<Country> GetCountryById(int countryId);
        Task<DeleteCountryModel> DeleteCreateForm(int countryId);
        Task Delete(int countryId);

    }
}
