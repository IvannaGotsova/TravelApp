using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Data.Entities
{
    /// <summary>
    /// This is mapping class.
    /// </summary>
    public class ApplicationUserJourney
    {
        [Required]
        public string ApplicationUserId { get; set; } = null!;
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser? ApplicationUser { get; set; }
        [Required]
        public int JourneyId { get; set; }
        [ForeignKey(nameof(JourneyId))]
        public Journey? Journey { get; set; }
    }
}
