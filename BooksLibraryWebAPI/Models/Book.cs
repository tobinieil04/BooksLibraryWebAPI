﻿using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Xunit.Sdk;

namespace BooksLibraryWebAPI.Models
{
    public class Book
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
