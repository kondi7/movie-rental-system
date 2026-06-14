using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.Data;
using MovieRentalSystem.Models;

namespace MovieRentalSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly VideoRentalContext _context;

        public RentalsController(VideoRentalContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()// GET: Rentals
        {
            var rentals = await _context.Rentals
                .Include(r => r.Movie)
                .Include(r => r.Customer)
                .OrderByDescending(r => r.RentalDate)
                .ToListAsync();
            return View(rentals);
        }
        
        public async Task<IActionResult> Details(int? id)// GET: Rentals/Details/1
        {
            if (id == null) return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.Movie)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rental == null) return NotFound();

            return View(rental);
        }
        
        public async Task<IActionResult> Create()// GET: Rentals/Create
        {
            await PopulateDropdowns();
            return View();
        }
        
        [HttpPost]// POST: Rentals/Create
        public async Task<IActionResult> Create(int MovieId, int CustomerId)
        {
            var rental = new Rental// Manual model creation
            {
                MovieId = MovieId,
                CustomerId = CustomerId
            };

            var movie = await _context.Movies.FindAsync(MovieId);

            rental.RentalDate = DateTime.Now;
            rental.IsReturned = false;
            rental.TotalPrice = movie.PricePerDay;
    
            movie.IsAvailable = false;

            _context.Add(rental);
            _context.Update(movie);
    
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Return(int? id)// GET: Rentals/Return/1
        {
            if (id == null) return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.Movie)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rental == null) return NotFound();

            return View(rental);
        }
        
        [HttpPost, ActionName("Return")]// POST: Rentals/Return/1
        public async Task<IActionResult> ReturnConfirmed(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rental == null) return NotFound();

            rental.ReturnDate = DateTime.Now;
            rental.IsReturned = true;

            var days = (rental.ReturnDate.Value - rental.RentalDate).Days;
            if (days < 1) days = 1;
            rental.TotalPrice = rental.Movie.PricePerDay * days;

            rental.Movie.IsAvailable = true;

            _context.Update(rental);
            _context.Update(rental.Movie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(int? id)// GET: Rentals/Delete/1
        {
            if (id == null) return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.Movie)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rental == null) return NotFound();

            return View(rental);
        }
        
        [HttpPost, ActionName("Delete")]// POST: Rentals/Delete/1
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rental != null)
            {
                if (!rental.IsReturned)
                {
                    rental.Movie.IsAvailable = true;
                    _context.Update(rental.Movie);
                }

                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        
        private async Task PopulateDropdowns(int? selectedCustomerId = null, int? selectedMovieId = null)
        {
            var customers = await _context.Customers.ToListAsync();
            var availableMovies = await _context.Movies.Where(m => m.IsAvailable).ToListAsync();
            
            ViewData["CustomerId"] = new SelectList(
                customers.Select(c => new { 
                    Id = c.Id, 
                    FullName = $"{c.FirstName} {c.LastName}" 
                }), 
                "Id", 
                "FullName",
                selectedCustomerId
            );
            
            ViewData["MovieId"] = new SelectList(availableMovies, "Id", "Title", selectedMovieId);
        }
    }
}