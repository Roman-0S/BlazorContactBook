using BlazorContactBook.Models;

namespace BlazorContactBook.Services.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> CreateContactAsync(Contact contact);

        Task AddCategoriesToContactAsync(int contactId, string userId, IEnumerable<int> categoryIds);

        Task RemoveCategoriesFromContactAsync(int contactId, string userId);

        Task<IEnumerable<Contact>> GetContactsAsync(string userId);

        Task<Contact?> GetContactByIdAsync(int contactId, string userId);

        /// <summary>
        /// Retrieves all contacts that belong to a given category
        /// </summary>
        /// <param name="categoryId">The ID of the category to search</param>
        /// <param name="userId">The ID of the user</param>
        /// <returns>A collection of contacts belong to the given category</returns>
        Task<IEnumerable<Contact>> GetContactsByCategoryIdAsync(int categoryId, string userId);

        Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm, string userId);

        Task UpdateContactAsync(Contact contact);


        Task DeleteContactAsync(int contactId, string userId);

    }
}
