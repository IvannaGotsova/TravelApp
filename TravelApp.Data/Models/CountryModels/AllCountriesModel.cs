using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using static TravelApp.Data.DataConstants.DataConstants.CountryConstants;

namespace TravelApp.Data.Models.CountryModels
{
    public class AllCountriesModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(CountryMaxLengthName, MinimumLength = CountryMinLengthName)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(typeof(long), CountryMinPopulation, CountryMinPopulation, ConvertValueInInvariantCulture = true)]
        public long Population { get; set; }
        [Required]
        [Range(typeof(long), CountryMinArea, CountryMaxArea, ConvertValueInInvariantCulture = true)]
        public long Area { get; set; }
        [Required]
        [StringLength(CountryMaxLengthImage, MinimumLength = CountryMinLengthImage)]
        public string Image { get; set; } = null!;
        [Required]
        [StringLength(CountryMaxLengthDescription, MinimumLength = CountryMinLengthDescription)]
        public string Description { get; set; } = null!;
    }
}
