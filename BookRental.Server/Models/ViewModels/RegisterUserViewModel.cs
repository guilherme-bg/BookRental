using System.ComponentModel.DataAnnotations;

namespace BookRental.Server.Models.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "UserName is required!")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public required string Password { get; set; }
    }
}
