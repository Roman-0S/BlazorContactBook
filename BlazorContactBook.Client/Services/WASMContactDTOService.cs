using BlazorContactBook.Client.Models;
using BlazorContactBook.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorContactBook.Client.Services
{
    public class WASMContactDTOService : IContactDTOService
    {
        private readonly HttpClient _httpClient;

        public WASMContactDTOService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ContactDTO> CreateContactAsync(ContactDTO contact, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(int contactId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailContactAsync(int contactId, EmailData emailData, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ContactDTO?> GetContactByIdAsync(int contactId, string userId)
        {
            ContactDTO? contact = await _httpClient.GetFromJsonAsync<ContactDTO>($"api/Contacts/{contactId}");

            return contact;
        }

        public async Task<IEnumerable<ContactDTO>> GetContactsAsync(string userId)
        {
            IEnumerable<ContactDTO> contacts = await _httpClient.GetFromJsonAsync<IEnumerable<ContactDTO>>("api/Contacts") ?? [];

            return contacts;
        }

        public async Task<IEnumerable<ContactDTO>> GetContactsByCategoryIdAsync(int categoryId, string userId)
        {
            IEnumerable<ContactDTO>? contacts = await _httpClient.GetFromJsonAsync<IEnumerable<ContactDTO>>($"api/Contacts/categoryId={categoryId}");

            return contacts!;
        }

        public async Task<IEnumerable<ContactDTO>> SearchContactsAsync(string searchTerm, string userId)
        {
            IEnumerable<ContactDTO>? contacts = await _httpClient.GetFromJsonAsync<IEnumerable<ContactDTO>>($"api/Contacts/searchTerm={searchTerm}");

            return contacts!;
        }

        public Task UpdateContactAsync(ContactDTO contact, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
