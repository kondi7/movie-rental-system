using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.Data;
using MovieRentalSystem.Models;

namespace MovieRentalSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly VideoRentalContext _context;

        public CustomersController(VideoRentalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()// GET: Customers
        {
            var customers = await _context.Customers
                .Include(c => c.Rentals)
                .ToListAsync();
            return View(customers);
        }
        
        public async Task<IActionResult> Details(int? id)// GET: Customers/Details/1
        {
            if (id == null) return NotFound();

            var customer = await _context.Customers
                .Include(c => c.Rentals)
                    .ThenInclude(r => r.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (customer == null) return NotFound();

            return View(customer);
        }
        
        public IActionResult Create()// GET: Customers/Create
        {
            return View();
        }

        
        [HttpPost]// POST: Customers/Create
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.RegisteredDate = DateTime.Now;
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        
        public async Task<IActionResult> Edit(int? id)// GET: Customers/Edit/1
        {
            if (id == null) return NotFound();

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            return View(customer);
        }
        
        [HttpPost]// POST: Customers/Edit/1
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(int? id)// GET: Customers/Delete/1
        {
            if (id == null) return NotFound();

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]// POST: Customers/Delete/1
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}