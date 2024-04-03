using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] AppDbContext context)
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }

        
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id, [FromServices] AppDbContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            
            return Ok(category);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category , [FromServices] AppDbContext context)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            
            return CreatedAtRoute("Get", new { id = category.Id }, category);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category category,[FromServices] AppDbContext context)
        {
            var categoryToUpdate = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            categoryToUpdate!.Name = category.Name;
            categoryToUpdate.Posts = category.Posts;
            categoryToUpdate.Slug = category.Slug;
            await context.SaveChangesAsync();
            
            return Ok(categoryToUpdate);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromServices] AppDbContext context)
        {
            var categoryToDelete = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            context.Categories.Remove(categoryToDelete!);
            await context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
