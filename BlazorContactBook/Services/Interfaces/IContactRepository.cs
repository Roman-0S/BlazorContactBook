using BlazorContactBook.Models;

namespace BlazorContactBook.Services.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> CreateContactAsync(Contact contact);

        Task AddCategoriesToContactAsync(int contactId, string userId, IEnumerable<int> categoryIds);

        Task<IEnumerable<Contact>> GetContactsAsync(string userId);

        Task UpdateContactAsync(Contact contact);

        Task<Contact?> GetContactByIdAsync(int contactId, string userId);
    }
}
