using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.Data;
using MovieRentalSystem.Models;

namespace MovieRentalSystem.Controllers
{
    public class MoviesController : Controller
    {
        private readonly VideoRentalContext _context;

        public MoviesController(VideoRentalContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()// GET: Movies
        {
            var movies = await _context.Movies.ToListAsync();
            return View(movies);
        }
        
        public async Task<IActionResult> Details(int? id)// GET: Movies/Details/1
        {
            if (id == null) return NotFound();

            var movie = await _context.Movies
                .Include(m => m.Rentals)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (movie == null) return NotFound();

            return View(movie);
        }
        
        public IActionResult Create()// GET: Movies/Create
        {
            return View();
        }
        
        [HttpPost]// POST: Movies/Create
        public async Task<IActionResult> Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.IsAvailable = true;
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        
        public async Task<IActionResult> Edit(int? id)// GET: Movies/Edit/1
        {
            if (id == null) return NotFound();

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();
            
            return View(movie);
        }
        
        [HttpPost]// POST: Movies/Edit/1
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        
        public async Task<IActionResult> Delete(int? id)// GET: Movies/Delete/1
        {
            if (id == null) return NotFound();

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return NotFound();

            return View(movie);
        }
        
        [HttpPost, ActionName("Delete")]// POST: Movies/Delete/1
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}