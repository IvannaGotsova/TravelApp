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
    public interface ITownService
    {
        Task<IEnumerable<AllTownsModel>> GetAllTowns();
        Task Add(AddTownModel addTownModel);
        Task<IEnumerable<Town>> GetTownsForSelect();
        Task<DetailsTownModel> GetTownDetailsById(int townId);
        Task<EditTownModel> EditCreateForm(int townId);
        Task Edit(int townId, EditTownModel editTownModel);
        Task<Town> GetTownById(int townId);
        Task<DeleteTownModel> DeleteCreateForm(int townId);
        Task Delete(int townId);

    }
}
