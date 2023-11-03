using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDetyra.Models;

namespace MovieDetyra.Controllers
{
    [ApiController]
    [Route("api/Movie")]
    public class MovieController : Controller
    {
       

        private readonly ApplicationDbContext _context;
        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }
    

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _context.movies.Include(b => b.Director).ToListAsync();
            return Ok(movies);
        }

      

        [HttpGet("FilterById")]
        public IActionResult GetMoviesByYear(int? vitiLeshimit)
        {
            if (!vitiLeshimit.HasValue)
            {
                return NotFound("Nuk keni shtypur asnje vit");
            }
            var filteredMovies = _context.movies.Include(b=>b.Director).Where(b => b.ReleaseYear == vitiLeshimit).ToList();
            return Ok(filteredMovies);
        }

        [HttpGet("FilterByAuthor")]
        public IActionResult GetMoviesByAuthor(string? director)
        {
            if (director ==null)
            {
                return NotFound("Nuk u gjet asnje autor me kete emer");
            }
            var filteredAuthors = _context.movies.Include(b => b.Director).Where(b => b.Director.Name == director).ToList();
            return Ok(filteredAuthors);
        }
        [HttpPost]
        public async Task<ActionResult<Movie>>CreateMovies(Movie movie)
        {
            _context.movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovies), new { id = movie.MovieId }, movie);
        }
    }
}
