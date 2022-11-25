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
    public class CustomerBookingsController : Controller
    {
        private readonly GoTravelContext _context;

        public CustomerBookingsController(GoTravelContext context)
        {
            _context = context;
        }

        //// GET: CustomerBookings
        //public async Task<IActionResult> CustomerHomePage(int customerId)
        //{
        //    var goTravelContext = _context.CustomerBookings.Include(c => c.Booking).Include(c => c.Customer);
        //    var customerBookings = await goTravelContext.ToListAsync();
        //    var curBookings = new List<CustomerBooking>();
        //    foreach (CustomerBooking cur in customerBookings)
        //    {
        //        if (cur.CustomerId == customerId)
        //            curBookings.Add(cur);
        //    }
        //    ViewData["loggedCustomerId"] = customerId;
        //    return View(curBookings);
        //}

        // GET: CustomerBookings
        public async Task<IActionResult> CustomerHomePage(int? id)
        {
            var goTravelContext = _context.CustomerBookings.Include(c => c.Booking).Include(c => c.Customer);
            var customerBookings = await goTravelContext.ToListAsync();
            var curBookings = new List<CustomerBooking>();
            foreach (CustomerBooking cur in customerBookings)
            {
                if (cur.CustomerId == id)
                    curBookings.Add(cur);
            }
            ViewData["loggedCustomerId"] = id;
            return View(curBookings);
        }

        public IActionResult ToCart(int? id)
        {
            return RedirectToAction("Index", "CartBookings", new { id = id });
        }

        public IActionResult CreateNew(int? id)
        {
            return RedirectToAction("Create", "CustomerBookings", new { id = id });
        }

        // GET: CustomerBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerBooking = await _context.CustomerBookings
                .Include(c => c.Booking)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CustomerBookingId == id);
            if (customerBooking == null)
            {
                return NotFound();
            }

            return View(customerBooking);
        }

        // GET: CustomerBookings/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "Description");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Password");
            return View();
        }

        // POST: CustomerBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerBookingId,PurchaseDate,Status,BookingId,CustomerId")] CustomerBooking customerBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction("CustomerHomePage", new { id = customerBooking.CustomerId });
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "Description", customerBooking.BookingId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Password", customerBooking.CustomerId);
            return View(customerBooking);
        }

        // GET: CustomerBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerBooking = await _context.CustomerBookings.FindAsync(id);
            if (customerBooking == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "Description", customerBooking.BookingId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Password", customerBooking.CustomerId);
            return View(customerBooking);
        }

        // POST: CustomerBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerBookingId,PurchaseDate,Status,BookingId,CustomerId")] CustomerBooking customerBooking)
        {
            if (id != customerBooking.CustomerBookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerBookingExists(customerBooking.CustomerBookingId))
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
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "Description", customerBooking.BookingId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Password", customerBooking.CustomerId);
            return View(customerBooking);
        }

        // GET: CustomerBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerBooking = await _context.CustomerBookings
                .Include(c => c.Booking)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CustomerBookingId == id);
            if (customerBooking == null)
            {
                return NotFound();
            }

            ViewData["loggedCustomerId"] = customerBooking.CustomerId;
            return View(customerBooking);
        }

        // POST: CustomerBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerBooking = await _context.CustomerBookings.FindAsync(id);
            _context.CustomerBookings.Remove(customerBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction("CustomerHomePage", new { id = customerBooking.CustomerId });
        }

        private bool CustomerBookingExists(int id)
        {
            return _context.CustomerBookings.Any(e => e.CustomerBookingId == id);
        }
    }
}
