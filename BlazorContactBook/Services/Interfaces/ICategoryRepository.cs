﻿using BlazorContactBook.Models;

namespace BlazorContactBook.Services.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(Category category);

        Task<IEnumerable<Category>> GetCategoriesAsync(string userId);

        Task DeleteCategoryAsync(int categoryId, string userId);

        Task UpdateCategoryAsync(Category category, string userId);

        Task<Category?> GetCategoryByIdAsync(int categoryId, string userId);
    }
}
