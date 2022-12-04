using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using static TravelApp.Data.DataConstants.DataConstants.JourneyConstants;

namespace TravelApp.Data.Models.JourneyModels
{
    public class DetailsJourneyModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(JourneyMaxLengthTitle, MinimumLength = JourneyMinLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(JourneyMaxLengthDescription, MinimumLength = JourneyMinLengthDescription)]
        public string Description { get; set; } = null!;
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } 
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [Required]
        [Range(JourneyMinDays, JourneyMaxDays)]
        public int Days { get; set; }
        [Required]
        [Range(typeof(decimal), "0.00", "10000.00", ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }
        [Required]
        [Range(JourneyMinNumberPeople, JourneyMaxNumberPeople)]
        public int NumberOfPeople { get; set; }
        [Required]
        [StringLength(JourneyMaxLengthImage)]
        public string Image { get; set; } = null!;
        [Required]
        public string Countries { get; set; } = null!;
        [Required]
        public string Towns { get; set; } = null!;
        public IEnumerable<DetailsJourneyModel> JourneysForCountries { get; set; } = new List<DetailsJourneyModel>();
     

    }
}
