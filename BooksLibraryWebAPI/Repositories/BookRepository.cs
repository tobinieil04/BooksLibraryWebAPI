using BooksLibraryWebAPI.Data;
using BooksLibraryWebAPI.Interfaces;
using BooksLibraryWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksLibraryWebAPI.Repositories
{
    public class BookRepository: IBookRepository
    {

        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context) => _context = context;


        public async Task<IEnumerable<Book>> GetAllBooksAsync(string? search, string? sortBy, int? pageNumber, int? pageSize)
        {
            IQueryable<Book> query = _context.Books;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search));
            }

            query = sortBy switch
            {
                "title" => query.OrderBy(b => b.Title),
                "author" => query.OrderBy(b => b.Author),
                "year" => query.OrderBy(b => b.PublishedYear),
                _ => query.OrderBy(b => b.Id) // Default
            };

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id) => await _context.Books.FindAsync(id);

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
