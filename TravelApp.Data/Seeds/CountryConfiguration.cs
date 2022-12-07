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
    /// This class holds Country Configuration.
    /// </summary>
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(CreateCountries());
        }

        private List<Country> CreateCountries()
        {
            var countries = new List<Country>()
            {
                new Country()
                {
                    Id = 1,
                    Name = "Bulgaria",
                    Description = "Country in Europe",
                    Population = 6823493,
                    Area = 110994,
                    Image = "/Photos/Bulgaria.jpg?a=123456"
                },

                new Country()
                {
                    Id = 2,
                    Name = "Croatia",
                    Description = "Country in Europe",
                    Population = 4044782,
                    Area = 56594,
                    Image = "/Photos/Croatia.jpg?a=123456"
                },

                new Country()
                {
                    Id = 3,
                    Name = "USA",
                    Description = "Country in NorthAmerica",
                    Population = 335649823,
                    Area = 9833520,
                    Image = "/Photos/USA.jpg?a=123456"
                }
            };
                
            return countries;
        }
    }
}
