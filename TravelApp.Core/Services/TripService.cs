using Microsoft.EntityFrameworkCore;
using TravelApp.Core.Contracts;
using TravelApp.Data.Models.TripModels;
using TravelApp.Data.Repositories;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.TownModels;
using System.ComponentModel.Design;
using TravelApp.Data.Models.CommentModels;

namespace TravelApp.Core.Services
{
    public class TripService : ITripService
    {
        private readonly IRepository data;

        public TripService(IRepository data)
        {
            this.data = data;
        }

        public async Task Add(AddTripModel addTripModel, string currentUserId)
        {
            var tripToBeAdded = new Trip()
            {
                Title = addTripModel.Title,
                JourneyId = addTripModel.JourneyId,
                Rating = addTripModel.Rating,
                ApplicationUserId = currentUserId
            };

            await this.data.AddAsync(tripToBeAdded);
            await this.data.SaveChangesAsync();
        }

        public async Task Delete(int tripId)
        {
            await this.data.DeleteAsync<Trip>(tripId);
            await this.data.SaveChangesAsync();
        }

        public async Task<DeleteTripModel> DeleteCreateForm(int tripId)
        {
            var tripToBeDeleted = await
               GetTripById(tripId);

            var deleteTripModel = new DeleteTripModel()
            {
                Id = tripToBeDeleted.Id,
                Title = tripToBeDeleted.Title,
                Rating = tripToBeDeleted.Rating
            };

            return deleteTripModel;
        }

        public async Task Edit(int tripId, EditTripModel editTripModel)
        {
            var tripToBeEdited = await
                     GetTripById(tripId);

            tripToBeEdited.Title = editTripModel.Title;
            tripToBeEdited.Rating = editTripModel.Rating;
    

            this.data.Update(tripToBeEdited);
            await this.data.SaveChangesAsync();
        }

        public async Task<EditTripModel> EditCreateForm(int tripId)
        {
            var tripToBeEdited = await
             GetTripById(tripId);


            var editTripModel = new EditTripModel()
            {
                Title = tripToBeEdited.Title,
                Rating = tripToBeEdited.Rating,
                Journeys = new List<Journey>(),

            };

            return editTripModel;
        }
        public async Task<IEnumerable<AllTripsModel>> GetAllTrips(string currentUserId)
        {
            var trips = await data
                 .AllReadonly<Trip>()
                 .Where(t => t.ApplicationUserId == currentUserId)
                 .Include(t => t.ApplicationUser)
                 .Include(t => t.Journey)
                 .ToListAsync();

            return trips
                .Select(t => new AllTripsModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    JourneyName = t.Journey!.Title,                  
                })
                .ToList();
        }

        public async Task<Trip> GetTripById(int tripId)
        {
            var trip = await
              this.data
              .AllReadonly<Trip>()
              .Where(t => t.Id == tripId)
              .FirstOrDefaultAsync();

            if (trip == null)
            {
                throw new ArgumentNullException();
            }

            return trip;
        }

        public async Task<DetailsTripModel> GetTripDetailsById(int tripId)
        {
            var trip = await
             this.data
             .AllReadonly<Trip>()
             .Where(t => t.Id == tripId)
             .Select(t => new DetailsTripModel()
             {
                 Id = t.Id,
                 Title = t.Title,
                 JourneyName = t.Journey!.Title,
                 Rating = t.Rating,
                 ApplicationUser = t.ApplicationUser!.Id,
             })
             .FirstOrDefaultAsync();

            if (trip == null)
            {
                throw new ArgumentNullException();
            }

            return trip;
        }

        public async Task<IEnumerable<Trip>> GetTripsForSelect()
        {
            return await
               this.data
               .AllReadonly<Trip>()
               .ToListAsync();
        }
    }
}
