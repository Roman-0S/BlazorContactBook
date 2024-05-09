using BlazorContactBook.Client.Models;
using BlazorContactBook.Client.Services.Interfaces;
using BlazorContactBook.Helpers;
using BlazorContactBook.Models;
using BlazorContactBook.Services.Interfaces;

namespace BlazorContactBook.Services
{
    public class ContactDTOService(IContactRepository repository) : IContactDTOService
    {
        public async Task<ContactDTO> CreateContactAsync(ContactDTO contactDTO, string userId)
        {
            Contact newContact = new Contact()
            {
                FirstName = contactDTO.FirstName,
                LastName = contactDTO.LastName,
                BirthDate = contactDTO.BirthDate,
                Address1 = contactDTO.Address1,
                Address2 = contactDTO.Address2,
                City = contactDTO.City,
                State = contactDTO.State ?? Client.Models.Enums.State.AK,
                ZipCode = contactDTO.ZipCode ?? 0,
                Email = contactDTO.Email,
                PhoneNumber = contactDTO.PhoneNumber,
                Created = DateTimeOffset.Now,
                AppUserId = userId,
            };

            if (contactDTO.ImageURL.StartsWith("data:"))
            {
                newContact.Image = UploadHelper.GetImageUpload(contactDTO.ImageURL);
            }

            Contact createdContact = await repository.CreateContactAsync(newContact);

            IEnumerable<int> categoryIds = contactDTO.Categories.Select(c => c.Id);
            await repository.AddCategoriesToContactAsync(createdContact.Id, userId, categoryIds);

            return createdContact.ToDTO();

        }

        public async Task<IEnumerable<ContactDTO>> GetContactsAsync(string userId)
        {
            IEnumerable<Contact> contacts = await repository.GetContactsAsync(userId);

            List<ContactDTO> contactsDTO = new List<ContactDTO>();

            foreach (Contact contact in contacts)
            {
                ContactDTO contactDTO = contact.ToDTO();

                contactsDTO.Add(contactDTO);
            }

            return contactsDTO;
        }
    }
}
