using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDetyra.Models.DTO;
using MovieDetyra.Models.Entities;

namespace MovieDetyra.Controllers
{
    [ApiController]
    [Route("api/Movie")]
    public class MovieController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public MovieController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
            _context = context;
        }
    

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            var movies = await _context.movies.Include(b => b.Director).ToListAsync();
            var moviesDTO = _mapper.Map<IEnumerable<MovieDTO>>(movies);
            return Ok(moviesDTO);
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
        public async Task<ActionResult<MovieDTO>>CreateMovies(MovieDTO movieDTO)
        {
            var movie = _mapper.Map<Movie>(movieDTO);
            _context.movies.Add(movie);
            await _context.SaveChangesAsync();
            var newMovie = _mapper.Map<MovieDTO>(movie);
            return CreatedAtAction(nameof(GetMovies), new { id = movie.MovieId }, newMovie);
        }
    }
}
