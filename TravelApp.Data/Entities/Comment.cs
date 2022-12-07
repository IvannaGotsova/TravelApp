using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TravelApp.Data.DataConstants.DataConstants.CommentConstants;

namespace TravelApp.Data.Entities
{
    /// <summary>
    /// Holds properties of Comment.
    /// </summary>
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(CommentMaxLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(CommentMaxLengthDescription)]
        public string Description { get; set; } = null!;
        [Required]
        [StringLength(CommentMaxLengthAuthor)]
        public string Author { get; set; } = null!;
        [Required]
        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public Post? Post { get; set; } 
    }
}
