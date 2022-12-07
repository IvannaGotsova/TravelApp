using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TravelApp.Data.DataConstants.DataConstants.PostConstants;

namespace TravelApp.Data.Entities
{
    /// <summary>
    /// Holds properties of Post.
    /// </summary>
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(PostMaxLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(PostMaxLengthDescription)]
        public string Description { get; set; } = null!;
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
        [Required]
        [StringLength(PostMaxLengthImage)]
        public string Image { get; set; } = null!;
        [Required]
        public int TripId { get; set; }
        [ForeignKey(nameof(TripId))]
        public Trip? Trip { get; set; }

    }
}
