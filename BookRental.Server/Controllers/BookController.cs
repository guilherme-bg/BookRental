using BookRental.Server.Models.ViewModels;
using BookRental.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Server.Controllers
{
    [ApiController, Authorize]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
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
        public async Task<IActionResult> GetBookByIdAsync(int id)
        {
            var result = await _bookService.GetBookByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(new { Message = result.ErrorMessage });
            }
            return Ok(result.Data);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBookAsync([FromBody] CreateBookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _bookService.AddBookAsync(book);
            return Ok();
        }

        [HttpPost("{id}/update")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] EditBookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _bookService.UpdateBookAsync(id, book);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpPost("{id}/borrow")]
        public async Task<IActionResult> BorrowBook(int id)
        {
            var result = await _bookService.BorrowBookAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpPost("{id}/return")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var result = await _bookService.ReturnBookAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteBook(int id)
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
