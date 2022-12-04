using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using static TravelApp.Data.DataConstants.DataConstants.RequestConstants;

namespace TravelApp.Data.Models.RequestModels
{
    public class AddRequestModel
    {
        [Required]
        [Range(RequestMinNumberPeople, RequestMaxNumberPeople)]
        public int NumberOfPeople { get; set; }
        public int JourneyId { get; set; }
        public string? ApplicationUserId { get; set; }
     
    }
}
