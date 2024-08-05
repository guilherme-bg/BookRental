using System.ComponentModel.DataAnnotations;

namespace BookRental.Server.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Synopsis { get; set; }

        [Required]
        public required string AuthorName { get; set; }

        public bool IsBorrowed { get; set; } = default;
    }
}
