using AutoMapper;
using BooksLibraryWebAPI.DTOs;
using BooksLibraryWebAPI.Interfaces;
using BooksLibraryWebAPI.Models;

namespace BooksLibraryWebAPI.Services
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            var books = await _repository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookDTO?> GetBookByIdAsync(int id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            return _mapper.Map<BookDTO>(book);
        }

        public async Task AddBookAsync(BookDTO bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _repository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(BookDTO bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _repository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _repository.DeleteBookAsync(id);
        }
    }
}
