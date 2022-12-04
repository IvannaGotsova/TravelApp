using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;

namespace TravelApp.Data.Seeds
{
    internal class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasData(CreateTrips());
        }

        private List<Trip> CreateTrips()
        {
            var trips = new List<Trip>()
            {
                new Trip()
                {
                    Id = 1,
                    Title = "New York Trip",
                    ApplicationUserId = "fire8756-c198-4129-b3f3-b893d8395082",
                    Rating = 10,
                    JourneyId = 1
                },

                new Trip()
                {
                    Id = 2,
                    Title = "Trip to Los Angeles",
                    ApplicationUserId = "roof9675-c198-4129-b3f3-b893d8395082",
                    Rating = 10,
                    JourneyId = 2
                }
            };

            return trips;
        }
    }
}
