using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.DTOs
{
    public class CreateContactDto
    {
        [Required] public string FirstName { get; set; } = null!;
        [Required] public string LastName { get; set; } = null!;
        [Required, EmailAddress] public string Email { get; set; } = null!;
        [Required, Phone] public string Phone { get; set; } = null!;
        public string? Tag { get; set; }
    }
}
