using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TravelApp.Data.DataConstants.DataConstants.CommentConstants;

namespace TravelApp.Data.Models.CommentModels
{
    public class DeleteCommentModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(CommentMaxLengthTitle, MinimumLength = CommentMinLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(CommentMaxLengthDescription, MinimumLength = CommentMinLengthDescription)]
        public string Description { get; set; } = null!;
    }
}
