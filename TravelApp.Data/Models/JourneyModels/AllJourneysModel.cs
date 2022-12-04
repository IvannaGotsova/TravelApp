using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using static TravelApp.Data.DataConstants.DataConstants.JourneyConstants;

namespace TravelApp.Data.Models.JourneyModels
{
    public class AllJourneysModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(JourneyMaxLengthTitle, MinimumLength = JourneyMinLengthTitle)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(JourneyMaxLengthDescription, MinimumLength = JourneyMinLengthDescription)]
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        [Required]
        public string Countries { get; set; } = null!;
        [Required]
        public string Towns { get; set; } = null!;
    }
}
