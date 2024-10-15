using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survivor.Context;
using Survivor.Entities;

namespace Survivor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly SurviverDbContext _context;

        public CategoryController (SurviverDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<CategoryEntity>>> GetCategories()
        {
            return await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<CategoryEntity>> GetCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if(category is null)
                return NotFound();


            return Ok(category);
        }


        [HttpPost]

        public async Task<ActionResult<CategoryEntity>> PostCategory(CategoryEntity category)
        {
            category.CreatedDate = DateTime.Now;
            category.ModifiedDate = DateTime.Now;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new {id = category.Id}, category);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> PutCategory(int id, CategoryEntity category)
        {
            if (category is null)
                return BadRequest();

            category.ModifiedDate = DateTime.Now;
            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Categories.Any(x => x.Id == id && !x.IsDeleted))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();

        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
                return NotFound();

            category.IsDeleted = true;
            category.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }


    
}
