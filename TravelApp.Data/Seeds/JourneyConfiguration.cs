using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using System.Globalization;

namespace TravelApp.Data.Seeds
{
    /// <summary>
    /// This class holds Journey Configuration.
    /// </summary>
    internal class JourneyConfiguration : IEntityTypeConfiguration<Journey>
    {
        public void Configure(EntityTypeBuilder<Journey> builder)
        {
            builder.HasData(CreateJourneys());
        }

        private static List<Journey> CreateJourneys()
        {
            var journeys = new List<Journey>()
            {
                new Journey()
                {
                   Id = 1,
                   Title = "Journey through New York",
                   Description = "Travel New York. Experience the city with our great proposal. Dont miss it.",
                   StartDate = DateTime.ParseExact("01/01/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                   EndDate = DateTime.ParseExact("11/01/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                   Price = 10000,
                   NumberOfPeople = 20,
                   Days = 11,
                   Image = "/Photos/NewYork.jpg?a=123456",
                },

                new Journey  ()
                {
                   Id = 2,
                   Title = "Journey through Los Angeles",
                   Description = "Our new offer. Visit this amazing city. Experience this adventure.",
                   StartDate = DateTime.ParseExact("01/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                   EndDate = DateTime.ParseExact("11/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                   Price = 10000,
                   NumberOfPeople = 30,
                   Days = 11,
                   Image = "/Photos/LosAngeles.jpg?a=123456"
                },

                new Journey  ()
                {
                   Id = 3,
                   Title = "Journey through Europe",
                   Description = "Visit two beautiful countries. Experience this adventure.",
                   StartDate = DateTime.ParseExact("02/03/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                   EndDate = DateTime.ParseExact("09/03/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                   Price = 500,
                   NumberOfPeople = 20,
                   Days = 8,
                   Image = "/Photos/Zagreb.jpg?a=123456"
                }
            };

            return journeys;
        }
    }
}
