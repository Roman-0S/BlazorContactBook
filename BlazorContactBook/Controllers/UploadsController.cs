using BlazorContactBook.Data;
using BlazorContactBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

namespace BlazorContactBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UploadsController(ApplicationDbContext context) : ControllerBase
    {


        [HttpGet("{id:guid}")]
        [OutputCache(VaryByRouteValueNames = ["id"], Duration = 60 * 60)]
        public async Task<IActionResult> GetImage(Guid Id)
        {

            ImageUpload? image = await context.Images.FirstOrDefaultAsync(i => i.Id == Id);

            return image == null ? NotFound() : File(image.Data!, image.Type!);

        }


    }
}
