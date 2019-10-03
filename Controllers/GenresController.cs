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
    public class GenresController : ControllerBase
    {
        private readonly EntityBoxContext _context;
        
        public GenresController(EntityBoxContext context)
        {
            _context = context;

            if (!_context.Genres.Any())
            {
                _context.Genres.Add(new Genre
                {
                    Slug = "drama",
                    Title = "Drama",
                    Description = "A drama genre."
                });
                _context.Genres.Add(new Genre
                {
                    Slug = "fantasy",
                    Title = "Fantasy",
                    Description = "A fantasy genre."
                });
                _context.SaveChanges();
            }
        }
        
        /* GET /api/genres
        ========================================= */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }
        
        /* GET /api/genres/2
        ========================================= */
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenreAsync(long id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }
        
        /* POST /api/genres
        ========================================= */
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenreAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGenreAsync), new { id = genre.Id }, genre);
        }
        
        /* PUT /api/genres/2
        ========================================= */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenreAsync(long id, Genre genre)
        {
            if (id != genre.Id)
            {
                return BadRequest();
            }

            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        /* DELETE /api/genres/2
        ========================================= */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenreAsync(long id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}