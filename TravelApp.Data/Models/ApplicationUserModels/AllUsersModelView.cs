using System.ComponentModel.DataAnnotations;
using TravelApp.Data.Entities;
using static TravelApp.Data.DataConstants.DataConstants.ApplicationUserConstants;

namespace TravelApp.Data.Models.ApplicationUserModels
{
    public class AllUsersModelView
    {
        public string Id { get; set; } = null!;
        [Required]
        [StringLength(UserMaxLengthUsername, MinimumLength = UserMinLengthUsername)]
        public string UserName { get; set; } = null!;
        [Required]
        [StringLength(UserMaxLengthFirstName, MinimumLength = UserMinLengthFirstName)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(UserMaxLengthLastName, MinimumLength = UserMinLengthLastName)]
        public string? LastName { get; set; }
        public int TripsCount { get; set; }
        public string? IsVIP { get; set; }

    }
}
