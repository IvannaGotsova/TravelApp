using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using static TravelApp.Data.DataConstants.DataConstants.PostConstants;

namespace TravelApp.Data.Models.PostModels
{
    public class AllPostsModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(PostMaxLengthTitle, MinimumLength = PostMinLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(PostMaxLengthDescription, MinimumLength = PostMinLengthDescription)]
        public string Description { get; set; } = null!;
        [Required]
        [StringLength(PostMaxLengthImage)]
        public string Image { get; set; } = null!;
        [Required]
        public string TripName { get; set; } = null!;
        [Required]
        public int TripId { get; set; }
        public IEnumerable<Trip> Trips { get; set; } = new List<Trip>();

        public string Author { get; set; } = null!;

    }
}
