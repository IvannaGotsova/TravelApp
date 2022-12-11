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
    /// This class holds Town Configuration.
    /// </summary>
    internal class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasData(CreateTowns());
        }

        private static List<Town> CreateTowns()
        {
            var towns = new List<Town>()
            {
            
                new Town()
                {
                    Id = 1,
                    Name = "New York",
                    Population = 8930002,
                    Area = 77818,
                    Image = "/Photos/NewYork.jpg?a=123456",
                    Description = "City in USA",
                    CountryId = 3
                },
               
                new Town()
                {
                    Id = 2,
                    Name = "Los Angeles",
                    Population = 3919973,
                    Area = 121000,
                    Image = "/Photos/LosAngeles.jpg?a=123456",
                    Description = "City in USA",
                    CountryId = 3
                },
               
                new Town()
                {
                    Id = 3,
                    Name = "Plovdiv",
                    Population = 343424,
                    Area = 10200,
                    Image = "/Photos/Plovdiv.jpg?a=123456",
                    Description = "City in Bulgaria",
                    CountryId = 1
                },
               
                new Town()
                {
                    Id = 4,
                    Name = "Zagreb",
                    Population = 806341,
                    Area = 64100,
                    Image = "/Photos/Zagreb.jpg?a=123456",
                    Description = "City in Croatia",
                    CountryId = 2
                }
        };

            return towns;
        }
    }
}
