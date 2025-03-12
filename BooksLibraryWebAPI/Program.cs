using BooksLibraryWebAPI.Data;
using BooksLibraryWebAPI.Interfaces;
using BooksLibraryWebAPI.Repositories;
using BooksLibraryWebAPI.Services;
using Microsoft.EntityFrameworkCore;


namespace BooksLibraryWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Configure In-Memory Database
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("LibraryDb"));


            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookService, BookService>();

            builder.Services.AddControllers();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
