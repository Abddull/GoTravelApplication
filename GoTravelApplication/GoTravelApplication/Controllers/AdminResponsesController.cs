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
    public class AdminResponsesController : Controller
    {
        private readonly GoTravelContext _context;

        public AdminResponsesController(GoTravelContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Allows to view all of logged in moderators, admin responses
        /// </summary>
        /// <param name="id">logged in moderator id</param>
        /// <returns>opens page with list of responses</returns>
        public async Task<IActionResult> Index(int? id)
        {
            var goTravelContext = _context.AdminResponses.Include(a => a.Admin).Include(a => a.Moderator);
            ViewData["loggedModId"] = id;
            var matchingResponses = new List<AdminResponse>();
            foreach (AdminResponse currentResponse in await goTravelContext.ToListAsync())
            {
                if (currentResponse.ModeratorId == id)
                    matchingResponses.Add(currentResponse);
            }
            return View(matchingResponses);
        }

        public ActionResult ModBack(int? id)
        {
            return RedirectToAction("ModeratorHomePage", "Moderators", new { id = id });
        }

        // GET: AdminResponses/Details/5
        public async Task<IActionResult> Details(int? id, int modId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminResponse = await _context.AdminResponses
                .Include(a => a.Admin)
                .Include(a => a.Moderator)
                .FirstOrDefaultAsync(m => m.ResponseId == id);
            if (adminResponse == null)
            {
                return NotFound();
            }

            ViewData["loggedModId"] = modId;
            return View(adminResponse);
        }

        // GET: AdminResponses/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Administrators, "AdminId", "Password");
            ViewData["ModeratorId"] = new SelectList(_context.Moderators, "ModeratorId", "Password");
            return View();
        }

        // POST: AdminResponses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResponseId,Title,Description,ResponseTime,ModeratorId,AdminId")] AdminResponse adminResponse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminResponse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Administrators, "AdminId", "Password", adminResponse.AdminId);
            ViewData["ModeratorId"] = new SelectList(_context.Moderators, "ModeratorId", "Password", adminResponse.ModeratorId);
            return View(adminResponse);
        }

        // GET: AdminResponses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminResponse = await _context.AdminResponses.FindAsync(id);
            if (adminResponse == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Administrators, "AdminId", "Password", adminResponse.AdminId);
            ViewData["ModeratorId"] = new SelectList(_context.Moderators, "ModeratorId", "Password", adminResponse.ModeratorId);
            return View(adminResponse);
        }

        // POST: AdminResponses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResponseId,Title,Description,ResponseTime,ModeratorId,AdminId")] AdminResponse adminResponse)
        {
            if (id != adminResponse.ResponseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminResponse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminResponseExists(adminResponse.ResponseId))
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
            ViewData["AdminId"] = new SelectList(_context.Administrators, "AdminId", "Password", adminResponse.AdminId);
            ViewData["ModeratorId"] = new SelectList(_context.Moderators, "ModeratorId", "Password", adminResponse.ModeratorId);
            return View(adminResponse);
        }

        // GET: AdminResponses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminResponse = await _context.AdminResponses
                .Include(a => a.Admin)
                .Include(a => a.Moderator)
                .FirstOrDefaultAsync(m => m.ResponseId == id);
            if (adminResponse == null)
            {
                return NotFound();
            }

            return View(adminResponse);
        }

        // POST: AdminResponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminResponse = await _context.AdminResponses.FindAsync(id);
            _context.AdminResponses.Remove(adminResponse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminResponseExists(int id)
        {
            return _context.AdminResponses.Any(e => e.ResponseId == id);
        }
    }
}
