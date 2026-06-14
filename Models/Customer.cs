namespace MovieRentalSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime RegisteredDate { get; set; }
        
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}