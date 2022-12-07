using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TravelApp.Data.DataConstants.DataConstants.RequestConstants;

namespace TravelApp.Data.Entities
{
    /// <summary>
    /// Holds properties of Request.
    /// </summary>
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public int NumberOfPeople { get; set; }
        [Required]
        public int JourneyId { get; set; }
        [ForeignKey(nameof(JourneyId))]
        public Journey? Journey { get; set; }
        [Required]
        public string ApplicationUserId { get; set; } = null!;
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser? ApplicationUser { get; set; }
        [Required]
        public bool IsApproved { get; set; } = false;
        [Required]
        public bool IsManaged { get; set; } = false;
    }
}
