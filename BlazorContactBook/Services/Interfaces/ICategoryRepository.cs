using BlazorContactBook.Models;

namespace BlazorContactBook.Services.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(Category category);

        Task<IEnumerable<Category>> GetCategoriesAsync(string userId);
    }
}
