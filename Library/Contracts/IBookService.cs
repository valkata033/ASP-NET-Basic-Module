using Library.Data.Entities;
using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllAsync();

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task AddBookAsync(AddBookViewModel model);

        Task AddToCollectionAsync(int bookId, string userId);

        Task<IEnumerable<BookViewModel>> GetWatchedAsync(string userId);

        Task RemoveFromCollectionAsync(int bookId, string userId);
             
    }
}
