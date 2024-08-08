using BookRental.Server.Data;
using BookRental.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BookRentalTests.TestData
{
    public class TestBookDataFixture : IDisposable
    {
        public ApplicationDbContext ApplicationContext { get; private set; }

        public TestBookDataFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MovieListDatabase")
                .Options;

            ApplicationContext = new ApplicationDbContext(options);

            ApplicationContext.AddRange(
                       new Book { Name = "Book1", Synopsis = "Synopsis1", AuthorName = "Author1", IsBorrowed = default },
                           new Book { Name = "Book2", Synopsis = "Synopsis2", AuthorName = "Author2", IsBorrowed = default });
            ApplicationContext.SaveChanges();
        }

        public void Dispose()
        {
            ApplicationContext.Dispose();
        }
    }
}
