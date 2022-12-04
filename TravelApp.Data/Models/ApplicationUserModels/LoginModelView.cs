using System.ComponentModel.DataAnnotations;

namespace TravelApp.Data.Models.ApplicationUser
{
    public class LoginModelView
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

    }
}
