﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoTravelApplication.Model;

namespace GoTravelApplication.Controllers
{
    public class BookingsController : Controller
    {
        private readonly GoTravelContext _context;

        public BookingsController(GoTravelContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Allows to search through all existing bookings
        /// </summary>
        /// <param name="id">logged in customers Id</param>
        /// <param name="paraPriceLow">price floor filter</param>
        /// <param name="paraPriceHigh">price ceiling filter</param>
        /// <param name="paraStartLow">start date floor filter</param>
        /// <param name="paraStartHigh">start date ceiling filter</param>
        /// <param name="paraEndLow">end date floor filter</param>
        /// <param name="paraEndHigh">end date ceiling filter</param>
        /// <returns>All bookings that match parameters</returns>
        public async Task<IActionResult> Index(int? id, double paraPriceLow, double paraPriceHigh, DateTime? paraStartLow, DateTime? paraStartHigh, DateTime? paraEndLow, DateTime? paraEndHigh)
        {
            var bookings = await _context.Bookings.ToListAsync();
            if (paraPriceLow != 0 || paraPriceHigh != 0 || paraStartLow != null || paraStartHigh != null || paraEndLow != null || paraEndHigh != null)
            {
                foreach (Booking book in await _context.Bookings.ToListAsync())
                {
                    if (paraPriceLow != 0)
                    {
                        if (book.Price < paraPriceLow)
                            bookings.Remove(book);
                    }
                    if (paraPriceHigh != 0)
                    {
                        if (book.Price > paraPriceHigh)
                            bookings.Remove(book);
                    }
                    if (paraStartLow != null)
                    {
                        if (book.StartDate < paraStartLow)
                            bookings.Remove(book);
                    }
                    if (paraStartHigh != null)
                    {
                        if (book.StartDate > paraStartHigh)
                            bookings.Remove(book);
                    }
                    if (paraEndLow != null)
                    {
                        if (book.EndDate < paraEndLow)
                            bookings.Remove(book);
                    }
                    if (paraEndHigh != null)
                    {
                        if (book.EndDate > paraEndHigh)
                            bookings.Remove(book);
                    }
                }
            }
            ViewData["loggedCustomerId"] = id;
            return View(bookings);
        }

        /// <summary>
        /// Adds selected booking to cart
        /// </summary>
        /// <param name="id">logged in customers id</param>
        /// <param name="bookId">booking id to be added to cart</param>
        /// <returns>Booking search page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int? id, int? bookId)
        {
            CartBooking cartBooking = new CartBooking();
            if (bookId != null)
                cartBooking.BookingId = (int)bookId;
            if (id != null)
                cartBooking.CustomerId = (int)id;
            _context.Add(cartBooking);
            await _context.SaveChangesAsync();
            ViewData["loggedCustomerId"] = id;
            return RedirectToAction("Index", new { id = id });
        }

        public IActionResult Back(int? id)
        {
            return RedirectToAction("CustomerHomePage", "CustomerBookings", new { id = id });
        }

        /// <summary>
        /// Allows to search through all existing bookings. Used by admin
        /// </summary>
        /// <param name="id">logged in admins Id</param>
        /// <param name="paraPriceLow">price floor filter</param>
        /// <param name="paraPriceHigh">price ceiling filter</param>
        /// <param name="paraStartLow">start date floor filter</param>
        /// <param name="paraStartHigh">start date ceiling filter</param>
        /// <param name="paraEndLow">end date floor filter</param>
        /// <param name="paraEndHigh">end date ceiling filter</param>
        /// <returns>All bookings that match parameters</returns>
        public async Task<IActionResult> AdminSearch(int? id, double paraPriceLow, double paraPriceHigh, DateTime? paraStartLow, DateTime? paraStartHigh, DateTime? paraEndLow, DateTime? paraEndHigh)
        {
            var bookings = await _context.Bookings.ToListAsync();
            if (paraPriceLow != 0 || paraPriceHigh != 0 || paraStartLow != null || paraStartHigh != null || paraEndLow != null || paraEndHigh != null)
            {
                foreach (Booking book in await _context.Bookings.ToListAsync())
                {
                    if (paraPriceLow != 0)
                    {
                        if (book.Price < paraPriceLow)
                            bookings.Remove(book);
                    }
                    if (paraPriceHigh != 0)
                    {
                        if (book.Price > paraPriceHigh)
                            bookings.Remove(book);
                    }
                    if (paraStartLow != null)
                    {
                        if (book.StartDate < paraStartLow)
                            bookings.Remove(book);
                    }
                    if (paraStartHigh != null)
                    {
                        if (book.StartDate > paraStartHigh)
                            bookings.Remove(book);
                    }
                    if (paraEndLow != null)
                    {
                        if (book.EndDate < paraEndLow)
                            bookings.Remove(book);
                    }
                    if (paraEndHigh != null)
                    {
                        if (book.EndDate > paraEndHigh)
                            bookings.Remove(book);
                    }
                }
            }
            ViewData["loggedAdminId"] = id;
            return View(bookings);
        }

        /// <summary>
        /// Opens page to create new booking
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <returns>create page</returns>
        public IActionResult AdminCreate(int? id)
        {
            ViewData["loggedAdminId"] = id;
            return View();
        }

        /// <summary>
        /// Creates new booking
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <param name="booking">booking to be created</param>
        /// <returns>admins booking search page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminCreate(int? id, [Bind("BookingId,Title,Description,Price,StartDate,EndDate")] Booking booking)
        {
            ViewData["loggedAdminId"] = id;
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("AdminSearch", new { id = id });
            }
            return View(booking);
        }

        /// <summary>
        /// opens bookings details page. Used by admin
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <param name="bookId">booking to be viewed id</param>
        /// <returns>details page</returns>
        public async Task<ActionResult> AdminDetails(int? id, int? bookId)
        {
            var booking = await _context.Bookings.FindAsync(bookId);
            ViewData["loggedAdminId"] = id;
            return View(booking);
        }

        /// <summary>
        /// Opens edit page for booings. Used by admins
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <param name="bookId">bookingid to be edited</param>
        /// <returns>edit page</returns>
        public async Task<ActionResult> AdminEdit(int? id, int? bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(bookId);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["loggedAdminId"] = id;
            return View(booking);
        }

        /// <summary>
        /// Edits existing booking
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <param name="booking">booking to be saved</param>
        /// <returns>admindetails page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoEdit(int id, [Bind("BookingId,Title,Description,Price,StartDate,EndDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["loggedAdminId"] = id;
                return RedirectToAction("AdminDetails", new { id = id, bookId = booking.BookingId });
            }
            ViewData["loggedAdminId"] = id;
            return View(booking);
        }

        public ActionResult AdminBack(int? id)
        {
            return RedirectToAction("AdminHomePage", "Administrators", new { id = id });
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id, int? bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.BookingId == bookId);
            if (booking == null)
            {
                return NotFound();
            }

            ViewData["loggedCustomerId"] = id;
            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,Title,Description,Price,StartDate,EndDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,Title,Description,Price,StartDate,EndDate")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
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
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
