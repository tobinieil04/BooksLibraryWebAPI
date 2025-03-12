using BooksLibraryWebAPI.Interfaces;
using BooksLibraryWebAPI.Models;
using BooksLibraryWebAPI.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BooksLibraryWebAPI.DTOs;


namespace BooksLibraryWebAPI.Tests
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _repoMock;
        private readonly BookService _service;

        public BookServiceTests()
        {
            _repoMock = new Mock<IBookRepository>();
            _service = new BookService(_repoMock.Object);
        }

        [Theory]
        [InlineData("Test Book 1", "Author A", 2022, "Fiction")]
        [InlineData("Test Book 2", "Author B", 2020, "Science")]
        [InlineData("Test Book 3", "Author C", 2019, "Programming")]
        public async Task AddBookAsync_ShouldCallRepositoryWithMappedBook(string title, string author, int year, string genre)
        {

            var bookDto = new BookDTO { Title = title, Author = author, PublishedYear = year, Genre = genre };

            await _service.AddBookAsync(bookDto);


            _repoMock.Verify(r => r.AddBookAsync(It.Is<Book>(
                b => b.Title == title && 
                     b.Author == author &&
                     b.PublishedYear == year &&
                     b.Genre == genre)),Times.Once);
        }


        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnBooks()
        {

            // Arrange
            var books = new List<Book>
            {
                new() { Id = 1, Title = "Test Book", Author = "Author A", PublishedYear = 2022, Genre = "Fiction" },
                new() { Id = 2, Title = "Another Book", Author = "Author B", PublishedYear = 2021, Genre = "Science" }
            };

            _repoMock.Setup(r => r.GetAllBooksAsync(null, null, null, null)).ReturnsAsync(books);

            // Act
            var result = await _service.GetAllBooksAsync(null, null, null, null);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Test Book", result.First().Title);
            Assert.Equal("Another Book", result.Last().Title);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnMappedBookDTO()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Test Book", Author = "Author A", PublishedYear = 2022, Genre = "Fiction" };
            _repoMock.Setup(r => r.GetBookByIdAsync(1)).ReturnsAsync(book);

            // Act
            var result = await _service.GetBookByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Title, result.Title);
            Assert.Equal(book.Author, result.Author);
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldCallRepositoryWithMappedBook()
        {
            // Arrange
            var bookDto = new BookDTO { Id = 1, Title = "Updated Title", Author = "Updated Author", PublishedYear = 2024, Genre = "Education" };

            // Act
            await _service.UpdateBookAsync(bookDto);

            // Assert
            _repoMock.Verify(r => r.UpdateBookAsync(It.Is<Book>(
                b => b.Id == bookDto.Id &&
                     b.Title == bookDto.Title &&
                     b.Author == bookDto.Author &&
                     b.PublishedYear == bookDto.PublishedYear &&
                     b.Genre == bookDto.Genre)), Times.Once);
        }

        [Fact]
        public async Task DeleteBookAsync_ShouldCallRepositoryWithCorrectId()
        {
            // Arrange
            int bookId = 1;

            // Act
            await _service.DeleteBookAsync(bookId);

            // Assert
            _repoMock.Verify(r => r.DeleteBookAsync(bookId), Times.Once);
        }
    }
}
