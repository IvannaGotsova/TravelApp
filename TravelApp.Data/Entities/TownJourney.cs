using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Data.Entities
{
    public class TownJourney
    {
        [Required]
        public int TownId { get; set; }
        [ForeignKey(nameof(TownId))]
        public Town? Town { get; set; }
        [Required]
        public int JourneyId { get; set; }
        [ForeignKey(nameof(JourneyId))]
        public Journey? Journey { get; set; }
    }
}
