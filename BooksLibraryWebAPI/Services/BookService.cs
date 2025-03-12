using BooksLibraryWebAPI.DTOs;
using BooksLibraryWebAPI.Interfaces;
using BooksLibraryWebAPI.Models;

namespace BooksLibraryWebAPI.Services
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _repository;


        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync(string? search, string? sortBy, int? pageNumber, int? pageSize)
        {
            var books = await _repository.GetAllBooksAsync(search, sortBy, pageNumber, pageSize);

            return books.Select(book => new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear,
                Genre = book.Genre
       
            });
            //return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookDTO?> GetBookByIdAsync(int id)
        {
            var book = await _repository.GetBookByIdAsync(id);

            return book == null ? null : new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear,
                Genre = book.Genre
            };
            //return _mapper.Map<BookDTO>(book);
        }

        public async Task AddBookAsync(BookDTO bookDto)
        {

            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                PublishedYear = bookDto.PublishedYear,
                Genre = bookDto.Genre
            };
            await _repository.AddBookAsync(book);
            //var book = _mapper.Map<Book>(bookDto);
            //await _repository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(BookDTO bookDto)
        {

            var book = new Book
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Author = bookDto.Author,
                PublishedYear = bookDto.PublishedYear,
                Genre = bookDto.Genre
            };
            //var book = _mapper.Map<Book>(bookDto);
            await _repository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _repository.DeleteBookAsync(id);
        }
    }
}
