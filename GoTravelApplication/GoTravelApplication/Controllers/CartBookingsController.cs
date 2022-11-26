using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoTravelApplication.Model;

namespace GoTravelApplication.Controllers
{
    public class CartBookingsController : Controller
    {
        private readonly GoTravelContext _context;

        public CartBookingsController(GoTravelContext context)
        {
            _context = context;
        }

        // GET: CartBookings
        public async Task<IActionResult> Index(int? id)
        {
            var goTravelContext = _context.CartBookings.Include(c => c.Booking).Include(c => c.Customer);
            var cartBookings = await goTravelContext.ToListAsync();
            var curBookings = new List<CartBooking>();
            foreach (CartBooking cur in cartBookings)
            {
                if (cur.CustomerId == id)
                    curBookings.Add(cur);
            }
            ViewData["loggedCustomerId"] = id;
            return View(curBookings);
        }

        // GET: CartBookings
        public async Task<IActionResult> Checkout(int? id)
        {
            var goTravelContext = _context.CartBookings.Include(c => c.Booking).Include(c => c.Customer);
            var cartBookings = await goTravelContext.ToListAsync();
            var curBookings = new List<CartBooking>();
            foreach (CartBooking cur in cartBookings)
            {
                if (cur.CustomerId == id)
                {
                    var customerBooking = new CustomerBooking();
                    customerBooking.PurchaseDate = DateTime.Now;
                    customerBooking.Status = "Unused";
                    customerBooking.BookingId = cur.BookingId;
                    customerBooking.CustomerId = cur.CustomerId;
                    _context.Add(customerBooking);
                    _context.Remove(cur);
                    await _context.SaveChangesAsync();
                }
            }
            ViewData["loggedCustomerId"] = id;

            return RedirectToAction("CustomerHomePage", "CustomerBookings", new { id = id });
        }
        public IActionResult Back(int? id)
        {
            return RedirectToAction("CustomerHomePage", "CustomerBookings", new { id = id });
        }

        // GET: CartBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartBooking = await _context.CartBookings
                .Include(c => c.Booking)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cartBooking == null)
            {
                return NotFound();
            }

            return View(cartBooking);
        }

        // GET: CartBookings/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "Description");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Password");
            return View();
        }

        // POST: CartBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,BookingId,CustomerId")] CartBooking cartBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "Description", cartBooking.BookingId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Password", cartBooking.CustomerId);
            return View(cartBooking);
        }

        // GET: CartBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartBooking = await _context.CartBookings.FindAsync(id);
            if (cartBooking == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "Description", cartBooking.BookingId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Password", cartBooking.CustomerId);
            return View(cartBooking);
        }

        // POST: CartBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,BookingId,CustomerId")] CartBooking cartBooking)
        {
            if (id != cartBooking.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartBookingExists(cartBooking.CartId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "Description", cartBooking.BookingId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Password", cartBooking.CustomerId);
            return View(cartBooking);
        }

        // GET: CartBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartBooking = await _context.CartBookings
                .Include(c => c.Booking)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cartBooking == null)
            {
                return NotFound();
            }

            ViewData["loggedCustomerId"] = cartBooking.CustomerId;
            return View(cartBooking);
        }

        // POST: CartBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartBooking = await _context.CartBookings.FindAsync(id);
            _context.CartBookings.Remove(cartBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = cartBooking.CustomerId });
        }

        private bool CartBookingExists(int id)
        {
            return _context.CartBookings.Any(e => e.CartId == id);
        }
    }
}
