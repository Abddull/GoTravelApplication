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
    public class AdministratorsController : Controller
    {
        private readonly GoTravelContext _context;

        public AdministratorsController(GoTravelContext context)
        {
            _context = context;
        }

        // GET: Administrators
        public IActionResult Index(string msg)
        {
            ViewData["msg"] = msg;
            return View();
        }

        /// <summary>
        /// handles login functionality for administrators
        /// </summary>
        /// <param name="administrator">administrator object with username and password fields filled</param>
        /// <returns>if login succeed, id for the logged administrator is returned</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("AdministratorId,UserName,Password")] Administrator administrator)
        {
            Administrator loggedAdmin = null;
            var administrators = await _context.Administrators.ToListAsync();
            foreach (Administrator cur in administrators)
            {
                if (cur.UserName == administrator.UserName && cur.Password == administrator.Password)
                {
                    loggedAdmin = cur;
                    break;
                }
            }
            if (loggedAdmin == null)
                return RedirectToAction("Index", new { msg = "Login Credentials are incorrect" });
            return RedirectToAction("AdminHomePage", new { id = loggedAdmin.AdminId });
        }

        /// <summary>
        /// Loads administrator home page
        /// </summary>
        /// <param name="id">logged admins home page</param>
        /// <returns>the administrator home page</returns>
        public async Task<ActionResult> AdminHomePage(int? id)
        {
            var admin = await _context.Administrators.FindAsync(id);
            ViewData["loggedAdminId"] = id;
            return View(admin);
        }

        public ActionResult OpenCustomerSearchPage(int? id)
        {
            return RedirectToAction("AdminSearch", "Customers", new { id = id });
        }

        public ActionResult OpenBookingSearchPage(int? id)
        {
            return RedirectToAction("AdminSearch", "Bookings", new { id = id });
        }

        public ActionResult OpenModeratorSearchPage(int? id)
        {
            return RedirectToAction("AdminSearch", "Moderators", new { id = id });
        }

        public ActionResult OpenModeratorRequestPage(int? id)
        {
            return RedirectToAction("AdminOpen", "ModRequests", new { id = id });
        }

        // GET: Administrators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrators
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (administrator == null)
            {
                return NotFound();
            }

            return View(administrator);
        }

        // GET: Administrators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,UserName,Password")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(administrator);
        }

        // GET: Administrators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }
            return View(administrator);
        }

        // POST: Administrators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,UserName,Password")] Administrator administrator)
        {
            if (id != administrator.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministratorExists(administrator.AdminId))
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
            return View(administrator);
        }

        // GET: Administrators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrators
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (administrator == null)
            {
                return NotFound();
            }

            return View(administrator);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrator = await _context.Administrators.FindAsync(id);
            _context.Administrators.Remove(administrator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministratorExists(int id)
        {
            return _context.Administrators.Any(e => e.AdminId == id);
        }
    }
}
