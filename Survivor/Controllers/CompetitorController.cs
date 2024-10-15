using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survivor.Context;
using Survivor.Entities;

namespace Survivor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitorController : ControllerBase
    {
        private readonly SurviverDbContext _context;

        public CompetitorController(SurviverDbContext context)
        {
            _context = context;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<CompetitorEntity>>> GetCompetitors()
        {
            return await _context.Competitors.Where(x => x.IsDeleted).ToListAsync();
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<CompetitorEntity>> GetCompetitor(int id)
        {
            var competitor = await _context.Competitors.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (competitor is  null) 
                return NotFound();


            return Ok(competitor);
        }


        [HttpGet("categories/{categoryId}")]

        public async Task<ActionResult<IEnumerable<CompetitorEntity>>> GetCompetitorsByCategoryId(int categoryId)
        {
            return await _context.Competitors
                .Where(x => x.CategoryId == categoryId &&  !x.IsDeleted)
                .ToListAsync();
        }


        [HttpPost]

        public async Task<ActionResult<CompetitorEntity>> PostCompetitor(CompetitorEntity competitor)
        {
            competitor.CreatedDate = DateTime.Now;
            competitor.ModifiedDate = DateTime.Now;
            _context.Competitors .Add(competitor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompetitor), new {id = competitor.Id}, competitor);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> PutCompetitor(int id, CompetitorEntity competitor)
        {
            if (id != competitor.Id) 
                return BadRequest();

            competitor.ModifiedDate = DateTime.Now;
            _context.Entry(competitor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Competitors.Any(x => x.Id == id && !x.IsDeleted))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCompetitor(int id)
        {
            var competitor = await _context.Competitors.FindAsync(id);

            if (competitor is null)
                return NotFound();

            competitor.IsDeleted = true;
            competitor.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
