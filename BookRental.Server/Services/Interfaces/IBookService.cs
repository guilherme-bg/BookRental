using BookRental.Server.Helpers;
using BookRental.Server.Models;
using BookRental.Server.Models.ViewModels;

namespace BookRental.Server.Services.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// This method retrieves a list containing all registered books.
        /// </summary>
        Task<IList<Book>> GetAllBooksAsync();

        /// <summary>
        /// This method retrieves the book with the given id.
        /// </summary>
        Task<ServiceResult<Book>> GetBookByIdAsync(int id);

        /// <summary>
        /// This method registers a new book.
        /// </summary>
        Task<ServiceResult> AddBookAsync(CreateBookViewModel bookRequest);

        /// <summary>
        /// This method deletes the book with the given id.
        /// </summary>
        Task<ServiceResult> DeleteBookAsync(int id);

        /// <summary>
        /// This method updates the informations about the book that matches the given id.
        /// </summary>
        Task<ServiceResult<Book>> UpdateBookAsync(int id, EditBookViewModel book);

        /// <summary>
        /// This method changes the book IsBorrowed field to true.
        /// </summary>
        Task<ServiceResult> BorrowBookAsync(int id);

        /// <summary>
        /// This method changes the book IsBorrowed field to false.
        /// </summary>
        Task<ServiceResult> ReturnBookAsync(int id);
    }
}
