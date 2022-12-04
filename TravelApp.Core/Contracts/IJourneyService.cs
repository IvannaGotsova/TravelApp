using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.JourneyModels;
using TravelApp.Data.Models.TripModels;

namespace TravelApp.Core.Contracts
{
    public interface IJourneyService
    {
        Task<IEnumerable<AllJourneysModel>> GetAllJourneys();
        Task Add(AddJourneyModel addJourneyModel, string currentUserId);
        Task<IEnumerable<Journey>> GetJourneysForSelect();
        Task<DetailsJourneyModel> GetJourneyDetailsById(int journeyId);
        Task<EditJourneyModel> EditCreateForm(int journeyId);
        Task Edit(int journeyId, EditJourneyModel editJourneyModel);
        Task<Journey> GetJourneyById(int journeyId);
        Task<DeleteJourneyModel> DeleteCreateForm(int journeyId);
        Task Delete(int journeyId, string currentUserId);

    }
}
