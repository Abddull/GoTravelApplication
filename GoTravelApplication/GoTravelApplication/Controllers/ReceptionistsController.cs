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
    public class ReceptionistsController : Controller
    {
        private readonly GoTravelContext _context;
        private Receptionist loggedReceptionist;

        public ReceptionistsController(GoTravelContext context)
        {
            _context = context;
        }

        public IActionResult Index(string msg)
        {
            ViewData["msg"] = msg;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("ReceptionistId,UserName,Password")] Receptionist receptionist)
        {
            var receptionists = await _context.Receptionists.ToListAsync();
            foreach (Receptionist cur in receptionists)
            {
                if (cur.UserName == receptionist.UserName && cur.Password == receptionist.Password)
                {
                    loggedReceptionist = cur;
                    break;
                }
            }
            if (loggedReceptionist == null)
                return RedirectToAction("Index", new { msg = "Login Credentials are incorrect" });
            return RedirectToAction("ReceptionistHomePage", new { id = loggedReceptionist.ReceptionistId });
        }

        // GET: CustomerBookings
        public async Task<IActionResult> ReceptionistHomePage(int? id, string msg)
        {
            var receptionist = await _context.Receptionists.FindAsync(id);
            ViewData["loggedReceptionistId"] = id;
            ViewData["msg"] = msg;
            return View(receptionist);
        }

        public async Task<IActionResult> SearchPage(int? id, int paraId)
        {
            var bookings = await _context.Bookings.ToListAsync();
            if (paraId != 0)
            {
                foreach (Booking book in await _context.Bookings.ToListAsync())
                {
                    if (paraId != 0)
                    {
                        if (book.Price == paraId)
                            bookings.Remove(book);
                    }
                }
            }
            ViewData["loggedCustomerId"] = id;
            return View(bookings);
        }


        public async Task<IActionResult> BookingDetails(int? id, int? bookId)
        {
            if (id == null)
            {
                return RedirectToAction("ReceptionistHomePage", new { id = id, msg = "Booking with the specified Id was not found" });
            }

            var booking = await _context.CustomerBookings.FindAsync(bookId);
            if (booking == null)
            {
                return RedirectToAction("ReceptionistHomePage", new { id = id, msg = "Booking with the specified Id was not found" });
            }
            ViewData["loggedReceptionistId"] = id;
            return View(booking);
        }


        public async Task<IActionResult> EditBooking(int? id, int? bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            var booking = await _context.CustomerBookings.FindAsync(bookId);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["loggedReceptionistId"] = id;
            return View(booking);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBookingDetails(int? id, int? bookId, string status)
        {
            var customerBooking = await _context.CustomerBookings.FindAsync(bookId);
            var receptionistChange = new ReceptionistChange(); 
            receptionistChange.OldStatus = customerBooking.Status;
            receptionistChange.NewStatus = status;
            receptionistChange.ChangeTime = DateTime.Now;
            receptionistChange.ReceptionistId = (int)id;
            receptionistChange.CustomerBookingId = customerBooking.CustomerBookingId;
            customerBooking.Status = status;
            try
            {
                _context.Update(customerBooking);
                _context.Add(receptionistChange);
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
            ViewData["loggedReceptionistId"] = id;
            return RedirectToAction("BookingDetails", new { id = id, bookId = customerBooking.CustomerBookingId });
        }


        // GET: Receptionists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptionist = await _context.Receptionists
                .FirstOrDefaultAsync(m => m.ReceptionistId == id);
            if (receptionist == null)
            {
                return NotFound();
            }

            return View(receptionist);
        }

        // GET: Receptionists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Receptionists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceptionistId,UserName,Password")] Receptionist receptionist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receptionist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(receptionist);
        }

        // GET: Receptionists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptionist = await _context.Receptionists.FindAsync(id);
            if (receptionist == null)
            {
                return NotFound();
            }
            return View(receptionist);
        }

        // POST: Receptionists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReceptionistId,UserName,Password")] Receptionist receptionist)
        {
            if (id != receptionist.ReceptionistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionistExists(receptionist.ReceptionistId))
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
            return View(receptionist);
        }

        // GET: Receptionists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptionist = await _context.Receptionists
                .FirstOrDefaultAsync(m => m.ReceptionistId == id);
            if (receptionist == null)
            {
                return NotFound();
            }

            return View(receptionist);
        }

        // POST: Receptionists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receptionist = await _context.Receptionists.FindAsync(id);
            _context.Receptionists.Remove(receptionist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptionistExists(int id)
        {
            return _context.Receptionists.Any(e => e.ReceptionistId == id);
        }

        private bool CustomerBookingExists(int id)
        {
            return _context.CustomerBookings.Any(e => e.CustomerBookingId == id);
        }
    }
}
