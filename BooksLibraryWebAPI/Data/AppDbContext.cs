﻿using BooksLibraryWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksLibraryWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Book> Books { get; set; }

    }
}
