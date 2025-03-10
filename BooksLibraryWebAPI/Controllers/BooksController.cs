using BooksLibraryWebAPI.DTOs;
using BooksLibraryWebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksLibraryWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBookService _service;
        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks() => Ok(await _service.GetAllBooksAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _service.GetBookByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO bookDTO)
        {
            await _service.AddBookAsync(bookDTO);
            return CreatedAtAction(nameof(GetBook), new { id = bookDTO.Id }, bookDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _service.DeleteBookAsync(id);
            return NoContent();
        }





    }
}
