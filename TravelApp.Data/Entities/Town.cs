using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TravelApp.Data.DataConstants.DataConstants.TownConstants;

namespace TravelApp.Data.Entities
{
    /// <summary>
    /// Holds properties of Town.
    /// </summary>
    public class Town
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(TownMaxLengthName)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(typeof(long), TownMinPopulation, TownMaxPopulation, ConvertValueInInvariantCulture = true)]
        public long Population { get; set; }
        [Required]
        [Range(typeof(long), TownMinArea,TownMaxArea, ConvertValueInInvariantCulture = true)]
        public long Area { get; set; }
        [Required]
        [StringLength(TownMaxLengthDescription)]
        public string Description { get; set; } = null!;
        [Required]
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }
        [Required]
        [StringLength(TownMaxLengthImage)]
        public string Image { get; set; } = null!;

        public IEnumerable<TownJourney> TownsJourneys { get; set; } = new List<TownJourney>();


    }
}
