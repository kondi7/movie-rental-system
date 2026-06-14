namespace MovieRentalSystem.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Description { get; set; }
        
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}