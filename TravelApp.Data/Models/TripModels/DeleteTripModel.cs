using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using static TravelApp.Data.DataConstants.DataConstants.TripConstants;

namespace TravelApp.Data.Models.TripModels
{
    public class DeleteTripModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(TripMaxLengthTitle, MinimumLength = TripMinLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        public string JourneyName { get; set; } = null!;
        [Required]
        [Range(TripMinRating, TripMaxRating)]
        public int Rating { get; set; }
    }
}
