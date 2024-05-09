using BlazorContactBook.Client.Models;

namespace BlazorContactBook.Client.Services.Interfaces
{
    public interface IContactDTOService
    {
        Task<ContactDTO> CreateContactAsync(ContactDTO contact, string userId);

        Task<IEnumerable<ContactDTO>> GetContactsAsync(string userId);

    }
}
