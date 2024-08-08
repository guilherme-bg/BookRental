using BookRental.Server.Models;
using BookRental.Server.Models.ViewModels;

namespace BookRentalTests.TestData
{
    public static class BookTestData
    {
        public static CreateBookViewModel ValidCreateBookViewModel()
        {
            return new CreateBookViewModel
            {
                Name = "Name",
                AuthorName = "Jon Doe",
                Synopsis = "synopsis"
            };
        }

        public static CreateBookViewModel InValidCreateBookViewModel()
        {
            return new CreateBookViewModel
            {
                Name = string.Empty,
                AuthorName = string.Empty,
                Synopsis = string.Empty
            };
        }

        public static IList<Book> BooksMock()
        {
            return
            [
                new Book { Id = 1, Name = "Book1", AuthorName = "Author1", Synopsis = "Synopsis1" },
                new Book { Id = 2, Name = "Book2", AuthorName = "Author2", Synopsis = "Synopsis2" }
            ];
        }

        public static Book BookMock()
        {
            return new Book { Id = 1, Name = "Book1", AuthorName = "Author1", Synopsis = "Synopsis1" };
        }
    }
}
