namespace BookRental.Server.Models.ViewModels
{
    public class EditBookViewModel
    {
        public required string Name { get; set; }
        public required string Synopsis { get; set; }
        public required string AuthorName { get; set; }
    }
}
