using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Data.Models.RequestModels
{
    public class AllRequestsModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string JourneyName { get; set; } = null!;
        [Required]
        public string Status { get; set; } = null!;
        public decimal FinalSum { get; set; }
        public int JourneyId { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        [Required]
        public string Management { get; set; } = null!;
        
    }
}
