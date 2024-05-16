using BlazorContactBook.Client.Models;
using BlazorContactBook.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorContactBook.Client.Services
{
    public class WASMCategoryDTOService : ICategoryDTOService
    {
        private readonly HttpClient _httpClient;

        public WASMCategoryDTOService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category, string userId)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Categories", category);
            response.EnsureSuccessStatusCode();

            CategoryDTO? createdCategory = await response.Content.ReadFromJsonAsync<CategoryDTO>();
            return createdCategory!;
        }

        public async Task DeleteCategoryAsync(int categoryId, string userId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Categories/{categoryId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> EmailCategoryAsync(int categoryId, EmailData emailData, string userId)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Categories/{categoryId}/email", emailData);
            response.EnsureSuccessStatusCode();

            bool success = await response.Content.ReadFromJsonAsync<bool>();
            return success;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(string userId)
        {
            IEnumerable<CategoryDTO> categories = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDTO>>("api/Categories") ?? [];

            return categories;
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int categoryId, string userId)
        {
            CategoryDTO? category = await _httpClient.GetFromJsonAsync<CategoryDTO>($"api/Categories/{categoryId}");

            return category;
        }

        public async Task UpdateCategoryAsync(CategoryDTO category, string userId)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Categories/{category.Id}", category);
            response.EnsureSuccessStatusCode();
        }
    }
}
