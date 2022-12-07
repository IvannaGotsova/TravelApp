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
    /// <summary>
    /// This class holds Town - Journey Configuration.
    /// </summary>
    internal class TownJourneyConfiguration : IEntityTypeConfiguration<TownJourney>
    {
        public void Configure(EntityTypeBuilder<TownJourney> builder)
        {
            builder.HasData(CreateTownsJourneys());
        }

        private List<TownJourney> CreateTownsJourneys()
        {
            var townsJourneys = new List<TownJourney>()
            {
                new TownJourney()
                {
                    TownId = 1,
                    JourneyId = 1
                },

                new TownJourney()
                {
                    TownId = 2,
                    JourneyId = 2
                },

                 new TownJourney()
                {
                    TownId = 3,
                    JourneyId = 3
                },

                  new TownJourney()
                {
                    TownId = 4,
                    JourneyId = 3
                },
            };

            return townsJourneys;
        }
    }
}
