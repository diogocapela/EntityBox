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
    public class CountriesController : ControllerBase
    {
        private readonly EntityBoxContext _context;
        
        public CountriesController(EntityBoxContext context)
        {
            _context = context;

            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Slug = "portugal",
                    Name = "Portugal"
                });
                _context.Countries.Add(new Country
                {
                    Slug = "spain",
                    Name = "Spain"
                });
                _context.SaveChanges();
            }
        }
        
        /* GET /api/countries
        ========================================= */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }
        
        /* GET /api/countries/2
        ========================================= */
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountryAsync(long id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }
        
        /* POST /api/countries
        ========================================= */
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountryAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCountryAsync), new { id = country.Id }, country);
        }
        
        /* PUT /api/countries/2
        ========================================= */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryAsync(long id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        /* DELETE /api/countries/2
        ========================================= */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryAsync(long id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}