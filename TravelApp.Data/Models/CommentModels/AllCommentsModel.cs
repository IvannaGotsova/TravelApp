using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using static TravelApp.Data.DataConstants.DataConstants.CommentConstants;

namespace TravelApp.Data.Models.CommentModels
{
    public class AllCommentsModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(CommentMaxLengthTitle, MinimumLength = CommentMinLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(CommentMaxLengthAuthor, MinimumLength = CommentMinLengthAuthor)]
        public string Author { get; set; } = null!;
        [Required]
        public int PostId { get; set; }
        public IEnumerable<Post> Posts { get; set; } = new List<Post>();
    }
}
