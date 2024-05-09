using BlazorContactBook.Client.Models;
using BlazorContactBook.Client.Services.Interfaces;
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


            Contact createdContact = await repository.CreateContactAsync(newContact);


            IEnumerable<int> categoryIds = contactDTO.Categories.Select(c => c.Id);

            await repository.AddCategoriesToContactAsync(createdContact.Id, userId, categoryIds);

            // need image



            return createdContact.ToDTO();

        }
    }
}
