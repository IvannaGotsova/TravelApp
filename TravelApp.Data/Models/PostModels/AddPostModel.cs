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
    public class AddPostModel
    {
        [Required]
        [StringLength(PostMaxLengthTitle, MinimumLength = PostMinLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(PostMaxLengthDescription, MinimumLength = PostMinLengthDescription)]
        public string Description { get; set; } = null!;      
        [Required]
        [StringLength(PostMaxLengthImage, MinimumLength = PostMinLengthImage)]
        public string Image { get; set; } = null!;
        [Required]
        public int TripId { get; set; }
        [ForeignKey(nameof(TripId))]
        public Trip? Trip { get; set; }
        public IEnumerable<Trip> Trips { get; set; } = new List<Trip>();
    }
}
