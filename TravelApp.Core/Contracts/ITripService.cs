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
    public interface ITripService
    {
        Task<IEnumerable<AllTripsModel>> GetAllTrips(string currentUserId);
        Task Add(AddTripModel addTripModel, string currentUserId);
        Task<IEnumerable<Trip>> GetTripsForSelect();
        Task<DetailsTripModel> GetTripDetailsById(int tripId);
        Task<EditTripModel> EditCreateForm(int tripId);
        Task Edit(int tripId, EditTripModel editTripModel);
        Task<Trip> GetTripById(int tripId);
        Task<DeleteTripModel> DeleteCreateForm(int tripId);
        Task Delete(int tripId);
    }
}
