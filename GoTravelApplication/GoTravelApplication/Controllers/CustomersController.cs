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
    public class CustomersController : Controller
    {
        private readonly GoTravelContext _context;
        private Customer loggedCustomer;

        public CustomersController(GoTravelContext context)
        {
            _context = context;
        }

        // GET: Customers
        public IActionResult Index(string msg)
        {
            ViewData["msg"] = msg;
            return View();
        }

        /// <summary>
        /// handles login functionality for customers
        /// </summary>
        /// <param name="customer">customer object with username and password fields filled</param>
        /// <returns>if login succeed, id for the logged customer is returned</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("CustomerId,UserName,Password")] Customer customer)
        {
            var customers = await _context.Customers.ToListAsync();
            foreach (Customer cur in customers)
            {
                if (cur.UserName == customer.UserName && cur.Password == customer.Password)
                {
                    loggedCustomer = cur;
                    break;
                }
            }
            if (loggedCustomer == null)
                return RedirectToAction("Index", new { msg = "Login Credentials are incorrect" });
            return RedirectToAction("CustomerHomePage", "CustomerBookings", new { id = loggedCustomer.CustomerId });
        }

        /// <summary>
        /// Loads customer home page
        /// </summary>
        /// <param name="id">logged in customers id</param>
        /// <returns>Customer Home Page</returns>
        public async Task<IActionResult> CustomerHomePage(int? id)
        {
            var customerBookings = await _context.CustomerBookings.ToListAsync();
            var curBookings = new List<CustomerBooking>();
            foreach (CustomerBooking cur in customerBookings)
            {
                if (cur.CustomerId == loggedCustomer.CustomerId)
                    curBookings.Add(cur);
            }
            ViewData["loggedAdminId"] = id;
            return View(curBookings);
        }

        /// <summary>
        /// Page where admins can search and view all customer
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <param name="custId">parameter to search with customer id</param>
        /// <param name="username">parameter to search with customer username</param>
        /// <returns>page with all matching customers</returns>
        public async Task<ActionResult> AdminSearch(int? id, int? custId, string username)
        {
            var customers = await _context.Customers.ToListAsync();
            foreach (Customer cur in await _context.Customers.ToListAsync())
            {
                if (custId != 0 && custId != null)
                    if (custId != cur.CustomerId)
                        customers.Remove(cur);
                if (username != null && username != "")
                    if (username != cur.UserName)
                        customers.Remove(cur);
            }
            ViewData["loggedAdminId"] = id;
            return View(customers);
        }

        /// <summary>
        /// Opens page for admins to view selected customer details
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <param name="custId">customer to be viewed</param>
        /// <returns>page with customer details</returns>
        public async Task<ActionResult> AdminDetails(int? id, int? custId)
        {
            var goTravelContext = _context.CustomerBookings.Include(c => c.Booking).Include(c => c.Customer);
            var customer = await _context.Customers.FindAsync(custId);
            var customerBookings = await goTravelContext.ToListAsync();
            foreach(CustomerBooking cur in await goTravelContext.ToListAsync())
            {
                if (cur.CustomerId != custId)
                    customerBookings.Remove(cur);
            }
            ViewData["loggedAdminId"] = id;
            ViewData["customerId"] = custId;
            ViewData["username"] = customer.UserName;
            ViewData["password"] = customer.Password;
            return View(customerBookings);
        }

        /// <summary>
        /// Opens page to view customer booking details
        /// </summary>
        /// <param name="id">ogged in admins id</param>
        /// <param name="bookingId">selected customer booking id</param>
        /// <returns>Customerbooking details page</returns>
        public ActionResult AdminBookDetails(int? id, int? bookingId)
        {
            return RedirectToAction("AdminDetails", "CustomerBookings", new { id = id , bookingId = bookingId });
        }

        /// <summary>
        /// Opens page to edit selected customer
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <param name="custId">customer to be edited</param>
        /// <returns>customer profile edit page</returns>
        public async Task<ActionResult> AdminEdit(int? id, int? custId)
        {
            if (custId == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(custId);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["loggedAdminId"] = id;
            return View(customer);
        }

        /// <summary>
        /// Edits selected customer
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <param name="customer">customer object to use for update</param>
        /// <returns>updates customer and returns to search page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoEdit(int id, [Bind("CustomerId,UserName,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["loggedAdminId"] = id;
                return RedirectToAction("AdminDetails", new { id = id, custId = customer.CustomerId });
            }
            ViewData["loggedAdminId"] = id;
            return View(customer);
        }

        /// <summary>
        /// Returns admin to the admin home page
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <returns>admin home page</returns>
        public ActionResult AdminBack(int? id)
        {
            return RedirectToAction("AdminHomePage", "Administrators", new { id = id });
        }

        /// <summary>
        /// Opens create page for customerbookings
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <param name="custId">selected customers id</param>
        /// <returns>Create customerbooking page</returns>
        public ActionResult AdminCreate(int? id, int? custId)
        {
            return RedirectToAction("AdminCreate", "CustomerBookings", new { id = id, custId = custId });
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,UserName,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,UserName,Password")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
