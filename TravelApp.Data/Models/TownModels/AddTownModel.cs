using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using static TravelApp.Data.DataConstants.DataConstants.TownConstants;

namespace TravelApp.Data.Models.TownModels
{
    public class AddTownModel
    {
        [Required]
        [StringLength(TownMaxLengthName, MinimumLength = TownMinLengthName)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(typeof(long), TownMinPopulation, TownMaxPopulation, ConvertValueInInvariantCulture = true)]
        public long Population { get; set; }
        [Required]
        [Range(typeof(long), TownMinArea, TownMaxArea, ConvertValueInInvariantCulture = true)]
        public long Area { get; set; }
        [Required]
        [StringLength(TownMaxLengthDescription, MinimumLength = TownMinLengthDescription)]
        public string Description { get; set; } = null!;
        [Required]
        public int CountryId { get; set; }
        public IEnumerable<Country> Countries { get; set; } = new List<Country>();
        [Required]
        [StringLength(TownMaxLengthImage)]
        public string Image { get; set; } = null!;
    }
}
