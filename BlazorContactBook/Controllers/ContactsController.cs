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
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContacts()
        {
            try
            {
                IEnumerable<ContactDTO> contacts = await _contactDTOService.GetContactsAsync(_userId);

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        [HttpGet("categoryId={categoryId:int}")]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContactsByCategoryId([FromRoute] int categoryId)
        {
            try
            {
                IEnumerable<ContactDTO> contacts = await _contactDTOService.GetContactsByCategoryIdAsync(categoryId, _userId);

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        // GET: "api/contacts/5" -> a contact or 404
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

        // GET: "api/contacts/search?query=whatever" -> contacts matching the search query
        [HttpGet("searchTerm={searchTerm:string}")]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> SearchContacts([FromRoute] string searchTerm)
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

        // POST: "api/contacts" -> creates and returns the created contact
        //public async Task<ActionResult<>> CreateContact()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return Problem();
        //    }
        //}

        //// PUT: "api/contacts/5" -> updates the selected contact and returns Ok
        //public async Task<ActionResult> UpdateContact()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return Problem();
        //    }
        //}

        //// DELETE: "api/contacts/5" -> deletes the selected contact and returns NoContent
        //public async Task<ActionResult> DeleteContact()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return Problem();
        //    }
        //}

        //// POST: "api/contacts/5/email" -> sends an email to contact and returns Ok or BadRequest to indicate success or failure
        //public async Task<ActionResult> EmailContact()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return Problem();
        //    }
        //}
    }
}
