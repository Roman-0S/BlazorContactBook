using BlazorContactBook.Client.Models;

namespace BlazorContactBook.Client.Services.Interfaces
{
    public interface ICategoryDTOService
    {
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category, string userId);

        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(string userId);
    }
}
