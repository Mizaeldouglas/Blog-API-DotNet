using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] AppDbContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        
        [HttpGet("{id:int}", Name = "Get")]
        public async Task<IActionResult> Get(int id, [FromServices] AppDbContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);

                return Ok(category);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "05xEF1 - Não foi posivel encontrar categoria");
                throw;
            }
            catch (Exception e)
            {
                return StatusCode(500, "05xEF2 - falha no sevidor");
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category , [FromServices] AppDbContext context)
        {
            try
            {
                context.Categories.Add(category);
                await context.SaveChangesAsync();

                return CreatedAtRoute("Get", new { id = category.Id }, category);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "05xEF3 - Não foi posivel inserir categoria");
                throw;
            }
            catch (Exception e)
            {
                return StatusCode(500, "05xEF4 - falha no sevidor");
            }
        }

        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category category,[FromServices] AppDbContext context)
        {
            try
            {
                var categoryToUpdate = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);

                categoryToUpdate!.Name = category.Name;
                categoryToUpdate!.Slug = category.Slug;

                context.Categories.Update(categoryToUpdate!);

                await context.SaveChangesAsync();

                return Ok(categoryToUpdate);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "05xEF5 - Não foi posivel inserir categoria");
                throw;
            }
            catch (Exception e)
            {
                return StatusCode(500, "05xEF6 - falha no sevidor");
            }
        }

        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, [FromServices] AppDbContext context)
        {
            try
            {
                var categoryToDelete = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                context.Categories.Remove(categoryToDelete!);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "05xEF7 - Não foi posivel inserir categoria");
                throw;
            }
            catch (Exception e)
            {
                return StatusCode(500, "05xEF8 - falha no sevidor");
            }
        }
    }
}
