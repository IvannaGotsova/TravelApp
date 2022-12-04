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
    internal class CountryJourneyConfiguration : IEntityTypeConfiguration<CountryJourney>
    {
        public void Configure(EntityTypeBuilder<CountryJourney> builder)
        {
            builder.HasData(CreateCountriesJourneys());
        }

        private List<CountryJourney> CreateCountriesJourneys()
        {
            var countriesJourneys = new List<CountryJourney>()
            {
                new CountryJourney()
                {
                    CountryId = 3,
                    JourneyId = 1
                },

                new CountryJourney()
                {
                    CountryId = 3,
                    JourneyId = 2
                },

                 new CountryJourney()
                {
                    CountryId = 2,
                    JourneyId = 3
                },

                  new CountryJourney()
                {
                    CountryId = 1,
                    JourneyId = 3
                },
            };

            return countriesJourneys;
        }
    }
}

