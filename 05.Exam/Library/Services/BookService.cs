using Library.Contracts;
using Library.Data;
using Library.Data.Entities;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;

        public BookService(LibraryDbContext _context)
        {
            context = _context;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            var book = new Book()
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,  
                CategoryId = model.CategoryId,
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task AddToCollectionAsync(int bookId, string userId)
        {
            var user = await context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == bookId);

            if (book == null)
            {
                throw new ArgumentException("Invalid book Id");
            }

            if (!user.ApplicationUsersBooks.Any(x => x.BookId == bookId))
            {
                user.ApplicationUsersBooks.Add(new ApplicationUserBook()
                {
                    BookId = bookId,
                    Book = book,
                    ApplicationUserId = userId,
                    ApplicationUser = user
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            var books = await context.Books
                .Include(x => x.Category)
                .ToListAsync();

            return books.Select(x => new BookViewModel()
            {
                Title = x.Title,
                Id = x.Id,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Category = x.Category.Name,
                Rating = x.Rating,
                Author = x.Author
            });
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetWatchedAsync(string userId)
        {
            var user = await context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.ApplicationUsersBooks)
                .ThenInclude(x => x.Book)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id!");
            }

            return user.ApplicationUsersBooks
                .Select(x => new BookViewModel()
                {
                    Title = x.Book.Title,
                    Description = x.Book.Description,
                    Id = x.Book.Id,
                    Author = x.Book.Author,
                    Rating = x.Book.Rating,
                    ImageUrl = x.Book.ImageUrl,
                    Category = x.Book.Category.Name
                });
        }

        public async Task RemoveFromCollectionAsync(int bookId, string userId)
        {
            var user = await context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id");
            }

            var book = user.ApplicationUsersBooks.FirstOrDefault(x => x.BookId == bookId);

            if (book != null)
            {
                user.ApplicationUsersBooks.Remove(book);
                await context.SaveChangesAsync();
            }
        }
    }
}
