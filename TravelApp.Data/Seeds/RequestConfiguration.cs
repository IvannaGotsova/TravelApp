using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;

namespace TravelApp.Data.Seeds
{
    internal class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasData(CreateRequests());
        }

        private List<Request> CreateRequests()
        {
            var requests = new List<Request>()
            {

                new Request()
                {
                    Id = 1,
                    NumberOfPeople = 2,
                    JourneyId = 1,
                    ApplicationUserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    IsApproved = false,
                    IsManaged = false
                },

                new Request()
                {
                    Id = 2,
                    NumberOfPeople = 3,
                    JourneyId = 2,
                    ApplicationUserId = "fire8756-c198-4129-b3f3-b893d8395082",
                    IsApproved = false,
                    IsManaged = false
                },

                
        };

            return requests;
        }
    }
}
