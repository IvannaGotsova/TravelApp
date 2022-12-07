using Microsoft.AspNetCore.Identity;
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
    /// This class holds Application User - Journey Configuration.
    /// </summary>
    internal class ApplicationUserJourneyConfiguration : IEntityTypeConfiguration<ApplicationUserJourney>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserJourney> builder)
        {
            builder.HasData(CreateApplicationUsersJourneys());
        }

        private List<ApplicationUserJourney> CreateApplicationUsersJourneys()
        {
            var applicationUsersJourneys = new List<ApplicationUserJourney>()
            {
                new ApplicationUserJourney()
                {
                    ApplicationUserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    JourneyId = 1
                },

                new ApplicationUserJourney()
                {
                    ApplicationUserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    JourneyId = 2
                },

                new ApplicationUserJourney()
                {
                    ApplicationUserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    JourneyId = 3
                },
            };

            return applicationUsersJourneys;
        }
    }
}
