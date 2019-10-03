using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityBox.Models;
using EntityBox.Persistence;

namespace EntityBox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly EntityBoxContext _context;
        
        public MoviesController(EntityBoxContext context)
        {
            _context = context;

            if (!_context.Movies.Any())
            {
                _context.Movies.Add(new Movie
                {
                    Slug = "american-beauty",
                    Title = "American Beauty"
                });
                _context.Movies.Add(new Movie
                {
                    Slug = "12-angry-man",
                    Title = "12 Angry Man"
                });
                _context.SaveChanges();
            }
        }
        
        /* GET /api/movies
        ========================================= */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }
        
        /* GET /api/movies/2
        ========================================= */
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieAsync(long id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }
        
        /* POST /api/movies
        ========================================= */
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovieAsync), new { id = movie.Id }, movie);
        }
        
        /* PUT /api/movies/2
        ========================================= */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieAsync(long id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        /* DELETE /api/movies/2
        ========================================= */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(long id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}