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
    /// <summary>
    /// Holds Interface for Journey functionality.
    /// </summary>
    public interface IJourneyService
    {
        /// <summary>
        /// This method returns IEnumerable of all journeys.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AllJourneysModel>> GetAllJourneys();
        /// <summary>
        /// This method is used to add a journey.
        /// </summary>
        /// <param name="addJourneyModel"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        Task Add(AddJourneyModel addJourneyModel, string currentUserId);
        /// <summary>
        /// This method returns IEnumerable of all journeys used for Select.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Journey>> GetJourneysForSelect();
        /// <summary>
        /// This method returns Details of particular journey with given id.
        /// </summary>
        /// <param name="journeyId"></param>
        /// <returns></returns>
        Task<DetailsJourneyModel> GetJourneyDetailsById(int journeyId);
        /// <summary>
        /// This method creates form for editing a journey.
        /// </summary>
        /// <param name="journeyId"></param>
        /// <returns></returns>
        Task<EditJourneyModel> EditCreateForm(int journeyId);
        /// <summary>
        ///  This method is used to edit a particular journey with given id.
        /// </summary>
        /// <param name="journeyId"></param>
        /// <param name="editJourneyModel"></param>
        /// <returns></returns>
        Task Edit(int journeyId, EditJourneyModel editJourneyModel);
        /// <summary>
        /// This method returns a particular journey with given id.
        /// </summary>
        /// <param name="journeyId"></param>
        /// <returns></returns>
        Task<Journey> GetJourneyById(int journeyId);
        /// <summary>
        ///  This method creates form for deleting a journey.
        /// </summary>
        /// <param name="journeyId"></param>
        /// <returns></returns>
        Task<DeleteJourneyModel> DeleteCreateForm(int journeyId);
        /// <summary>
        /// This method deletes a particular journey with given id.
        /// </summary>
        /// <param name="journeyId"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        Task Delete(int journeyId, string currentUserId);

    }
}
