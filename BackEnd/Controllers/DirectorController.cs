using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieDetyra.Models.DTO;
using MovieDetyra.Models.Entities;

namespace MovieDetyra.Controllers
{
    [ApiController]
    [Route("api/Directors")]
    public class DirectorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public DirectorController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper??throw new ArgumentNullException(nameof(mapper));
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectorDTO>>> GetDirectors()
        {
            var directors = await _context.directors.ToListAsync();
            var directorsDTO = _mapper.Map<IEnumerable<DirectorDTO>>(directors);
            return Ok(directorsDTO);
        }


        [HttpPost]
        public async Task<ActionResult<DirectorDTO>> PostDirectors(DirectorDTO directorDTO)
        {
            var director = _mapper.Map<Director>(directorDTO);

            _context.directors.Add(director);

            await _context.SaveChangesAsync();

            var newDirector = _mapper.Map<DirectorDTO>(director);
            return CreatedAtAction(nameof(GetDirectors), new { id = director.DirectorId }, newDirector);
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
