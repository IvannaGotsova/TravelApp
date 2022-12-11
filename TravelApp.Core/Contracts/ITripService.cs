using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.PostModels;
using TravelApp.Data.Models.TownModels;
using TravelApp.Data.Models.TripModels;

namespace TravelApp.Core.Contracts
{
    /// <summary>
    /// Holds Interface for Trip functionality.
    /// </summary>
    public interface ITripService
    {
        /// <summary>
        /// This method returns IEnumerable of all trips of login user.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AllTripsModel>> GetAllTrips(string currentUserId);
        /// <summary>
        /// This method is used to add a trip.
        /// </summary>
        /// <param name="addTripModel"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        Task Add(AddTripModel addTripModel, string currentUserId);
        /// <summary>
        /// This method returns IEnumerable of all Trips used for Select.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Trip>> GetTripsForSelect(string userId);
        /// <summary>
        /// This method returns Details of particular trip with given id.
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        Task<DetailsTripModel> GetTripDetailsById(int tripId);
        /// <summary>
        /// This method creates form for editing a trip.
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        Task<EditTripModel> EditCreateForm(int tripId);
        /// <summary>
        /// This method is used to edit a particular trip with given id. 
        /// </summary>
        /// <param name="tripId"></param>
        /// <param name="editTripModel"></param>
        /// <returns></returns>
        Task Edit(int tripId, EditTripModel editTripModel);
        /// <summary>
        /// This method returns a particular trip with given id. 
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        Task<Trip> GetTripById(int tripId);
        /// <summary>
        /// This method creates form for deleting a trip.
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        Task<DeleteTripModel> DeleteCreateForm(int tripId);
        /// <summary>
        /// This method deletes a particular trip with given id. 
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        Task Delete(int tripId);
    }
}
