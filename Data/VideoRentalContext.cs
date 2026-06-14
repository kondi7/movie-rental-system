using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.Models;

namespace MovieRentalSystem.Data
{
    public class VideoRentalContext : DbContext
    {
        public VideoRentalContext(DbContextOptions<VideoRentalContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Rentals)
                .HasForeignKey(r => r.MovieId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Come and See", Director = "Elem Klimov", Year = 1985, Genre = "Drama", PricePerDay = 10.00m, Description = "A more realistic and at the same time a bit psychedelic depiction of war and violence" },
                new Movie { Id = 2, Title = "12 Angry Men", Director = "Sidney Lumet", Year = 1957, Genre = "Drama", PricePerDay = 10.00m, Description = "People argue over a death sentence." },
                new Movie { Id = 3, Title = "The Godfather", Director = "Francis Ford Coppola", Year = 1972, Genre = "Drama", PricePerDay = 10.00m, Description = "Mafia family saga" },
                new Movie { Id = 4, Title = "Werckmeister harmóniák", Director = "Béla Tarr", Year = 2000, Genre = "Drama", PricePerDay = 10.00m, Description = "A naive young man witnesses an escalation of violence in his small hometown following the arrival of a mysterious circus attraction." },
                new Movie { Id = 5, Title = "The Dark Knight", Director = "Christopher Nolan", Year = 2008, Genre = "Action", PricePerDay = 10.00m, Description = "Batman vs Joker" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Adam", LastName = "Adamiak", Email = "adamadamiak@gmail.com", Phone = "123456789", RegisteredDate = new DateTime(2026, 1, 15) },
                new Customer { Id = 2, FirstName = "Ewa", LastName = "Ewiak", Email = "ewaewiak@protonmail.com", Phone = "987654321", RegisteredDate = new DateTime(2026, 2, 10) }
            );
        }
    }
}