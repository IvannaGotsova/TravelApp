using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static TravelApp.Data.DataConstants.DataConstants.TripConstants;

namespace TravelApp.Data.Entities
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(TripMaxLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        public int JourneyId { get; set; }
        [ForeignKey(nameof(JourneyId))]
        public Journey? Journey { get; set; }
        [Required]
        public string ApplicationUserId { get; set; } = null!;
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser? ApplicationUser { get; set; }
        [Required]
        [Range(TripMinRating, TripMaxRating)]
        public int Rating { get; set; }
        public List<Post> Posts = new();

    }
}
