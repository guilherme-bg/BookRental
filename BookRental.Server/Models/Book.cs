using System.ComponentModel.DataAnnotations;

namespace BookRental.Server.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public required string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public required string Synopsis { get; set; }

        [Required(AllowEmptyStrings = false)]
        public required string AuthorName { get; set; }

        public bool IsBorrowed { get; set; } = default;
    }
}
