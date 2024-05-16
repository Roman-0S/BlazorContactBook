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
    public class CategoriesController : ControllerBase
    {
        private string _userId => User.GetUserId()!;

        private readonly ICategoryDTOService _categoryDTOService;

        public CategoriesController(ICategoryDTOService categoryDTOService)
        {
            _categoryDTOService = categoryDTOService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            try
            {
                IEnumerable<CategoryDTO> categories = await _categoryDTOService.GetCategoriesAsync(_userId);

                return Ok(categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO?>> GetCategoryById([FromRoute] int Id)
        {
            try
            {
                CategoryDTO? category = await _categoryDTOService.GetCategoryByIdAsync(Id, _userId);

                return Ok(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO category)
        {
            try
            {
                CategoryDTO createdCategory = await _categoryDTOService.CreateCategoryAsync(category, _userId);

                return Ok(createdCategory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategory([FromRoute] int Id, [FromBody] CategoryDTO category)
        {
            try
            {
                if (Id != category.Id)
                {
                    return BadRequest();
                }

                await _categoryDTOService.UpdateCategoryAsync(category, _userId);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory([FromRoute] int Id)
        {
            try
            {
                await _categoryDTOService.DeleteCategoryAsync(Id, _userId);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }


        [HttpPost("{id:int}/email")]
        public async Task<ActionResult<bool>> EmailCategory([FromRoute] int Id, [FromBody] EmailData emailData)
        {
            try
            {
                bool success = await _categoryDTOService.EmailCategoryAsync(Id, emailData, _userId);

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
