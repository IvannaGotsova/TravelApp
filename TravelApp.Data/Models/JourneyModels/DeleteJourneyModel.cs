using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TravelApp.Data.DataConstants.DataConstants.JourneyConstants;

namespace TravelApp.Data.Models.JourneyModels
{
    public class DeleteJourneyModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(JourneyMaxLengthTitle, MinimumLength = JourneyMinLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(JourneyMaxLengthDescription, MinimumLength = JourneyMinLengthDescription)]
        public string Description { get; set; } = null!;
    }
}
