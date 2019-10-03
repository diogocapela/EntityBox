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
    public class PersonsController : ControllerBase
    {
        private readonly EntityBoxContext _context;
        
        public PersonsController(EntityBoxContext context)
        {
            _context = context;

            if (!_context.Persons.Any())
            {
                _context.Persons.Add(new Person
                {
                    Slug = "john",
                    Name = "John"
                });
                _context.Persons.Add(new Person
                {
                    Slug = "doe",
                    Name = "Doe"
                });
                _context.SaveChanges();
            }
        }
        
        /* GET /api/persons
        ========================================= */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonsAsync()
        {
            return await _context.Persons.ToListAsync();
        }
        
        /* GET /api/persons/2
        ========================================= */
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPersonAsync(long id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }
        
        /* POST /api/persons
        ========================================= */
        [HttpPost]
        public async Task<ActionResult<Person>> PostPersonAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonAsync), new { id = person.Id }, person);
        }
        
        /* PUT /api/persons/2
        ========================================= */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonAsync(long id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        /* DELETE /api/persons/2
        ========================================= */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonAsync(long id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}