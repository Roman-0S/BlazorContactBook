using BlazorContactBook.Client.Models;
using BlazorContactBook.Client.Services.Interfaces;
using BlazorContactBook.Helpers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorContactBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private string _userId => User.GetUserId()!;

        private readonly IContactDTOService _contactDTOService;

        public ContactsController(IContactDTOService contactDTOService)
        {
            _contactDTOService = contactDTOService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContacts([FromQuery] int? categoryId)
        {
            try
            {
                if (categoryId is null)
                {
                    IEnumerable<ContactDTO> contacts = await _contactDTOService.GetContactsAsync(_userId);

                    return Ok(contacts);
                }
                else
                {
                    IEnumerable<ContactDTO> contacts = await _contactDTOService.GetContactsByCategoryIdAsync(categoryId.Value, _userId);

                    return Ok(contacts);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContactDTO?>> GetContactById([FromRoute] int Id)
        {
            try
            {
                ContactDTO? contact = await _contactDTOService.GetContactByIdAsync(Id, _userId);

                return Ok(contact);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> SearchContacts([FromQuery] string searchTerm)
        {
            try
            {
                IEnumerable<ContactDTO> contacts = await _contactDTOService.SearchContactsAsync(searchTerm, _userId);

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        [HttpPost]
        public async Task<ActionResult<ContactDTO>> CreateContact(ContactDTO contact)
        {
            try
            {
                ContactDTO createdContact = await _contactDTOService.CreateContactAsync(contact, _userId);

                return Ok(createdContact);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateContact([FromRoute] int Id, [FromBody] ContactDTO contact)
        {
            try
            {
                if (Id != contact.Id)
                {
                    return BadRequest();
                }

                await _contactDTOService.UpdateContactAsync(contact, _userId);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteContact([FromRoute] int Id)
        {
            try
            {
                await _contactDTOService.DeleteContactAsync(Id, _userId);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        [HttpPost("{id:int}/email")]
        public async Task<ActionResult<bool>> EmailContact([FromRoute] int Id, [FromBody] EmailData emailData)
        {
            try
            {
                bool success = await _contactDTOService.EmailContactAsync(Id, emailData, _userId);

                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }
    }
}
