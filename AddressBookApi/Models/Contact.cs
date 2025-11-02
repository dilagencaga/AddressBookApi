namespace AddressBookApi.Models
{
    public class Contact
    {
        public int Id { get; set; }                 // Otomatik arttıracağımız Id
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Tag { get; set; }       // İsteğe bağlı (Work/Family gibi)
    }
}
