using Microsoft.AspNetCore.Mvc;
using AddressBookApi.Models;
using AddressBookApi.DTOs;

namespace AddressBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        // BAŞTAN SEED (kalıcı değil ama her deploy/başlangıçta dolu başlar)
        private static readonly List<Contact> _contacts = new()
        {
            new Contact { Id = 1, FirstName = "Alan",      LastName = "Turing",  Email = "alan.turing@auring.com",     Phone = "+44 7711 123456", Tag = "Work"   },
            new Contact { Id = 2, FirstName = "Marie",     LastName = "Curie",   Email = "marie.curie@urielmail.com",  Phone = "+33 612 987654",  Tag = "Family" },
            new Contact { Id = 3, FirstName = "Katherine", LastName = "Johnson", Email = "katherine.johnson@abcc.com", Phone = "+1 540 789 6543", Tag = "Phone"  },
            new Contact { Id = 4, FirstName = "Charles",   LastName = "Darwin",  Email = "charles.darwin@adl.gov",     Phone = "+44 7911 654321", Tag = "Tag"    }
        };

        private static int _nextId = 5;

        // GET /api/Contacts
        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetAll() => Ok(_contacts);

        // GET /api/Contacts/{id}
        [HttpGet("{id}")]
        public ActionResult<Contact> GetById(int id)
            => _contacts.FirstOrDefault(x => x.Id == id) is { } c ? Ok(c) : NotFound();

        // GET /api/Contacts/search?q=alan
        [HttpGet("search")]
        public ActionResult<IEnumerable<Contact>> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return Ok(_contacts);
            q = q.ToLowerInvariant();
            var results = _contacts.Where(x =>
                x.FirstName.ToLower().Contains(q) ||
                x.LastName.ToLower().Contains(q) ||
                x.Email.ToLower().Contains(q) ||
                (x.Tag ?? "").ToLower().Contains(q));
            return Ok(results);
        }

        // POST /api/Contacts
        [HttpPost]
        public ActionResult<Contact> Create([FromBody] CreateContactDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var contact = new Contact
            {
                Id = _nextId++,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Tag = dto.Tag
            };
            _contacts.Add(contact);
            return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
        }

        // PUT /api/Contacts/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateContactDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var c = _contacts.FirstOrDefault(x => x.Id == id);
            if (c is null) return NotFound();
            c.FirstName = dto.FirstName;
            c.LastName = dto.LastName;
            c.Email = dto.Email;
            c.Phone = dto.Phone;
            c.Tag = dto.Tag;
            return NoContent();
        }

        // DELETE /api/Contacts/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var c = _contacts.FirstOrDefault(x => x.Id == id);
            if (c is null) return NotFound();
            _contacts.Remove(c);
            return NoContent();
        }
    }
}
