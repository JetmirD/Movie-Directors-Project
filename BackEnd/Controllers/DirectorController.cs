using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieDetyra.Models;

namespace MovieDetyra.Controllers
{
    [ApiController]
    [Route("api/Directors")]
    public class DirectorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DirectorController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Director>>> GetDirectors()
        {
            var directors = await _context.directors.ToListAsync();

            return Ok(directors);
        }


        [HttpPost]
        public async Task<ActionResult<Director>> PostDirectors(Director director)
        {
            _context.directors.Add(director);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDirectors), new { id = director.DirectorId }, director);
        }

        [HttpPut("{id}")]
        public  IActionResult UpdateAuthor(int id, [FromBody] Director updatedDirector)
        {
            if (id != updatedDirector.DirectorId)
            {
                return BadRequest();
            }

            var existingDirector =  _context.directors.Find(id);
            if (existingDirector == null)
            {
                return NotFound("Author doesn't exist");
            }

            existingDirector.Name = updatedDirector.Name;
            existingDirector.BirthYear = updatedDirector.BirthYear;


            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.directors.Any(e => e.DirectorId == id))
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
    }
}
