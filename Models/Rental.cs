namespace MovieRentalSystem.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsReturned { get; set; } = false;
        
        public Movie Movie { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
    }
}