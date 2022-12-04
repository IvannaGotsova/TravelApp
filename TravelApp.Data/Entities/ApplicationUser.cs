using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TravelApp.Data.DataConstants.DataConstants.ApplicationUserConstants;

namespace TravelApp.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(UserMaxLengthFirstName)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(UserMaxLengthLastName)]
        public string LastName { get; set; } = null!;
        public IEnumerable<Trip> Trips { get; set; } = new List<Trip>();
        public IEnumerable<ApplicationUserJourney> ApplicationUsersJourneys { get; set; } = new List<ApplicationUserJourney>();
        public IEnumerable<Request> Requests { get; set; } = new List<Request>();
        public bool IsVIP { get; set; } = false;
    }
}
