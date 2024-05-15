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

        public async Task<ContactDTO> CreateContactAsync(ContactDTO contact, string userId)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Contacts", contact);
            response.EnsureSuccessStatusCode();

            ContactDTO? createdContact = await response.Content.ReadFromJsonAsync<ContactDTO>();
            return createdContact!;
        }

        public async Task DeleteContactAsync(int contactId, string userId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/contacts/{contactId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> EmailContactAsync(int contactId, EmailData emailData, string userId)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Contacts/{contactId}", emailData);
            response.EnsureSuccessStatusCode();

            bool success = await response.Content.ReadFromJsonAsync<bool>();
            return success;
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
            IEnumerable<ContactDTO> contacts = await _httpClient.GetFromJsonAsync<IEnumerable<ContactDTO>>($"api/Contacts/Search?SearchTerm={searchTerm}") ?? [];

            return contacts;
        }

        public async Task UpdateContactAsync(ContactDTO contact, string userId)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Contacts/{contact.Id}", contact);
            response.EnsureSuccessStatusCode();
        }
    }
}
