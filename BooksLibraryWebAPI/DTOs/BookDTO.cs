using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace BooksLibraryWebAPI.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Range(1000, 9999, ErrorMessage = "Published year must be valid")]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }
    }
}
