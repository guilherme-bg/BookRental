using System.ComponentModel.DataAnnotations;

namespace BookRental.Server.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public required string Name { get; set; }
    }
}
