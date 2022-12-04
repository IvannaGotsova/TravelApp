using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using static TravelApp.Data.DataConstants.DataConstants.CommentConstants;

namespace TravelApp.Data.Models.CommentModels
{
    public class EditCommentModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(CommentMaxLengthTitle, MinimumLength = CommentMinLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(CommentMaxLengthDescription, MinimumLength = CommentMinLengthDescription)]
        public string Description { get; set; } = null!;
        [Required]
        public string CommentAuthor { get; set; } = null!;
        [Required]
        public int PostId { get; set; }
        public IEnumerable<Post> Posts { get; set; } = new List<Post>();
        [Required]
        public string ApplicationUserId { get; set; } = null!;
    }
}
