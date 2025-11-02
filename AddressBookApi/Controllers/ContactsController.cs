using Microsoft.AspNetCore.Mvc;
using AddressBookApi.Models;

namespace AddressBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private static readonly List<Contact> contacts = new();
        private static int nextId = 1;

        [HttpGet] public IActionResult GetAll() => Ok(contacts);

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var c = contacts.FirstOrDefault(x => x.Id == id);
            return c is null ? NotFound() : Ok(c);
        }

        [HttpPost]
        public IActionResult Create(Contact input)
        {
            input.Id = nextId++;
            contacts.Add(input);
            return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Contact input)
        {
            var c = contacts.FirstOrDefault(x => x.Id == id);
            if (c is null) return NotFound();
            c.Name = input.Name; c.Phone = input.Phone;
            c.Email = input.Email; c.Address = input.Address;
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var c = contacts.FirstOrDefault(x => x.Id == id);
            if (c is null) return NotFound();
            contacts.Remove(c);
            return NoContent();
        }

        // /api/contacts/search?name=ali
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name)
        {
            var q = name?.Trim() ?? "";
            var result = contacts.Where(x => x.Name
                .Contains(q, StringComparison.OrdinalIgnoreCase));
            return Ok(result);
        }
    }
}
