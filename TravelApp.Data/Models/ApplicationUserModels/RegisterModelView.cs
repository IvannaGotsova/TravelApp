using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using static TravelApp.Data.DataConstants.DataConstants.ApplicationUserConstants;

namespace TravelApp.Data.Models.ApplicationUser
{
    public class RegisterModelView
    {
        [Required]
        [StringLength(UserMaxLengthUsername, MinimumLength = UserMinLengthUsername)]
        public string UserName { get; set; } = null!;
        [Required]
        [StringLength(UserMaxLengthFirstName, MinimumLength = UserMinLengthFirstName)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(UserMaxLengthLastName, MinimumLength = UserMinLengthLastName)]
        public string LastName { get; set; } = null!;
        [Required]
        [EmailAddress]
        [StringLength(UserMaxLengthEmail, MinimumLength = UserMinLengthEmail)]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(UserMaxLengthPassword, MinimumLength = UserMinLengthPassword)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

    }
}
