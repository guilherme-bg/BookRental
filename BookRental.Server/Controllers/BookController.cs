using BookRental.Server.Models;
using BookRental.Server.Models.ViewModels;
using BookRental.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetBooksAsync()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookByIdAsync(int id)
        {
            var result = await _bookService.GetBookByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(new { Message = result.ErrorMessage });
            }
            return Ok(result.Data);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddBookAsync([FromBody] CreateBookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            await _bookService.AddBookAsync(book);
            return Ok();
        }

        [HttpPost("{id}/update")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] EditBookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var result = await _bookService.UpdateBookAsync(id, book);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok();
        }

        [HttpPost("{id}/borrow")]
        public async Task<ActionResult> BorrowBook(int id)
        {           
            var result = await _bookService.BorrowBookAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok();
        }

        [HttpPost("{id}/return")]
        public async Task<ActionResult> ReturnBook(int id)
        {
            var result = await _bookService.BorrowBookAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok();
        }

        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok();
        }
    }
}
