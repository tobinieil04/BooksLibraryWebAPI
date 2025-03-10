using BooksLibraryWebAPI.DTOs;

namespace BooksLibraryWebAPI.Interfaces
{
    public interface IBookService
    {

        Task<IEnumerable<BookDTO>> GetAllBooksAsync();
        Task<BookDTO?> GetBookByIdAsync(int id);
        Task AddBookAsync(BookDTO bookDTO);
        Task UpdateBookAsync(BookDTO bookDTo);
        Task DeleteBookAsync(int id);
    }
}
