using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TravelApp.Data.DataConstants.DataConstants.CountryConstants;


namespace TravelApp.Data.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(CountryMaxLengthName)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(typeof(long), CountryMinPopulation, CountryMaxPopulation, ConvertValueInInvariantCulture = true)]
        public long Population { get; set; }
        [Required]
        [Range(typeof(long), CountryMinArea, CountryMaxArea, ConvertValueInInvariantCulture = true)]
        public long Area { get; set; }
        public List<Town> Towns { get; set; } = new ();
        [Required]
        [StringLength(CountryMaxLengthImage)]
        public string Image { get; set; } = null!;
        [Required]
        [StringLength(CountryMaxLengthDescription)]
        public string Description { get; set; } = null!;
        public IEnumerable<CountryJourney> CountriesJourneys { get; set; } = new List<CountryJourney>();
    }
}
