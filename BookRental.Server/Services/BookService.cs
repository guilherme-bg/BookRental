using BookRental.Server.Data;
using BookRental.Server.Helpers;
using BookRental.Server.Models;
using BookRental.Server.Models.ViewModels;
using BookRental.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Server.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<ServiceResult<Book>> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book is null)
            {
                return ServiceResult<Book>.Failure(Constants.BOOK_NOT_FOUND_ERROR);
            }

            return ServiceResult<Book>.Success(book);
        }

        public async Task<ServiceResult> AddBookAsync(CreateBookViewModel bookRequest)
        {
            var book = new Book
            {
                AuthorName = bookRequest.AuthorName,
                Name = bookRequest.Name,
                Synopsis = bookRequest.Synopsis
            };

            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteBookAsync(int id)
        {
            var result = await GetBookByIdAsync(id);

            if (!result.IsSuccess)
            {
                return result;
            }

            _context.Remove(result);
            await _context.SaveChangesAsync();
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<Book>> UpdateBookAsync(int id, EditBookViewModel book)
        {
            var result = await GetBookByIdAsync(id);

            if (!result.IsSuccess)
            {
                return result;
            }

            var existingBook = result.Data;
            existingBook.Name = book.Name;
            existingBook.Synopsis = book.Synopsis;
            existingBook.AuthorName = book.AuthorName;

            _context.Update(existingBook);
            await _context.SaveChangesAsync();
            return ServiceResult<Book>.Success(existingBook);
        }

        public async Task<ServiceResult> BorrowBookAsync(int id)
        {
            var result = await GetBookByIdAsync(id);

            if (!result.IsSuccess)
            {
                return result;
            }

            var existingBook = result.Data;

            if (existingBook.IsBorrowed)
            {
                return ServiceResult<Book>.Failure(Constants.BOOK_BORROWED_ERROR);
            }

            existingBook.IsBorrowed = true;

            _context.Update(existingBook);
            await _context.SaveChangesAsync();
            return ServiceResult<Book>.Success();
        }

        public async Task<ServiceResult> ReturnBookAsync(int id)
        {
            var result = await GetBookByIdAsync(id);

            if (!result.IsSuccess)
            {
                return result;
            }

            var existingBook = result.Data;

            if (existingBook.IsBorrowed)
            {
                return ServiceResult<Book>.Failure(Constants.BOOK_NOT_BORROWED_ERROR);
            }

            existingBook.IsBorrowed = default;

            _context.Update(existingBook);
            await _context.SaveChangesAsync();
            return ServiceResult<Book>.Success();
        }
    }
}
