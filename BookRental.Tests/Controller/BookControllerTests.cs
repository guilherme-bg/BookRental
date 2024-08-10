using BookRental.Server.Controllers;
using BookRental.Server.Helpers;
using BookRental.Server.Models;
using BookRental.Server.Services.Interfaces;
using BookRentalTests.TestData;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BookRentalTests.Controller
{
    public class BookControllerTests
    {
        private readonly IBookService _bookService;
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _bookService = Substitute.For<IBookService>();
            _controller = new BookController(_bookService);
        }

        [Fact]
        public async Task GetBooksAsync_ShouldReturnOkWithBooks()
        {
            // Arrange
            var books = BookTestData.BooksMock();

            _bookService.GetAllBooksAsync().Returns(books);

            // Act
            var result = await _controller.GetBooksAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBooks = Assert.IsType<List<Book>>(okResult.Value);
            Assert.Equal(3, returnBooks.Count);
        }

        [Fact]
        public async Task GetBooksByNameAsync_ShouldReturnOkWithBooks()
        {
            // Arrange
            var books = BookTestData.BooksMock();

            _bookService.GetBooksByNameAsync(books.First().Name).Returns(BookTestData.BooksWithTheSameNameMock());

            // Act
            var result = await _controller.GetBooksByNameAsync(books.First().Name);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBooks = Assert.IsType<List<Book>>(okResult.Value);
            Assert.Equal(2, returnBooks.Count);
            Assert.Equal(books.First().Name, returnBooks.First().Name);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnOkWithBook_WhenBookExists()
        {
            // Arrange
            var book = BookTestData.BookMock();
            _bookService.GetBookByIdAsync(1).Returns(ServiceResult<Book>.Success(book));

            // Act
            var result = await _controller.GetBookByIdAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBook = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(book, returnBook);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            _bookService.GetBookByIdAsync(1).Returns(ServiceResult<Book>.Failure(Constants.BOOK_NOT_FOUND_ERROR));

            // Act
            var result = await _controller.GetBookByIdAsync(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.Equal("{ Message = Book not found. }", notFoundResult.Value.ToString());

        }

        [Fact]
        public async Task AddBookAsync_ShouldReturnOk_WhenModelStateIsValid()
        {
            // Arrange
            var book = BookTestData.ValidCreateBookViewModel();
            await _bookService.AddBookAsync(book);

            // Act            
            var result = await _controller.AddBookAsync(book);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnOk_WhenBookIsUpdated()
        {
            // Arrange            
            var updateFields = BookTestData.ValidEditBookViewModel();
            _bookService.UpdateBookAsync(1, updateFields).Returns(ServiceResult<Book>.Success(new Book { Id = 1, AuthorName = updateFields.AuthorName, Name = updateFields.Name, Synopsis = updateFields.Synopsis }));

            // Act
            var result = await _controller.UpdateBook(1, updateFields);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var book = BookTestData.ValidEditBookViewModel();
            _bookService.UpdateBookAsync(1, book).Returns(ServiceResult<Book>.Failure(Constants.BOOK_NOT_FOUND_ERROR));

            // Act
            var result = await _controller.UpdateBook(1, book);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(Constants.BOOK_NOT_FOUND_ERROR, notFoundResult.Value);
        }

        [Fact]
        public async Task BorrowBook_ShouldReturnOk_WhenBookIsBorrowed()
        {
            // Arrange
            _bookService.BorrowBookAsync(1).Returns(ServiceResult.Success());

            // Act
            var result = await _controller.BorrowBook(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task BorrowBook_ShouldReturnBadRequest_WhenBookIsAlreadyBorrowed()
        {
            // Arrange
            _bookService.BorrowBookAsync(1).Returns(ServiceResult.Failure(Constants.BOOK_BORROWED_ERROR));

            // Act
            var result = await _controller.BorrowBook(1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(Constants.BOOK_BORROWED_ERROR, badRequestResult.Value);
        }

        [Fact]
        public async Task ReturnBook_ShouldReturnOk_WhenBookIsReturned()
        {
            // Arrange
            _bookService.ReturnBookAsync(1).Returns(ServiceResult.Success());

            // Act
            var result = await _controller.ReturnBook(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task ReturnBook_ShouldReturnBadRequest_WhenBookIsNotBorrowed()
        {
            // Arrange
            _bookService.ReturnBookAsync(1).Returns(ServiceResult.Failure(Constants.BOOK_NOT_BORROWED_ERROR));

            // Act
            var result = await _controller.ReturnBook(1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(Constants.BOOK_NOT_BORROWED_ERROR, badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnOk_WhenBookIsDeleted()
        {
            // Arrange
            _bookService.DeleteBookAsync(1).Returns(ServiceResult.Success());

            // Act
            var result = await _controller.DeleteBook(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            _bookService.DeleteBookAsync(1).Returns(ServiceResult.Failure(Constants.BOOK_NOT_FOUND_ERROR));

            // Act
            var result = await _controller.DeleteBook(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(Constants.BOOK_NOT_FOUND_ERROR, notFoundResult.Value);
        }
    }
}
