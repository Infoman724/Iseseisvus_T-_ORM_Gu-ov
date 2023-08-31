using Iseseisvus_Töö_ORM_Gužov.Data;
using Iseseisvus_Töö_ORM_Gužov.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iseseisvus_Töö_ORM_Gužov.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        //base
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        //create
        [HttpPost]
        public async Task<IActionResult> CreatePerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
        }

        //get/read
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        //update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, Person updatedPerson)
        {
            if (id != updatedPerson.Id)
                return BadRequest();

            _context.Entry(updatedPerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        //delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
                return NotFound();

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //check
        private bool PersonExists(int id)
        {
            return _context.Persons.Any(p => p.Id == id);
        }
    }


}
