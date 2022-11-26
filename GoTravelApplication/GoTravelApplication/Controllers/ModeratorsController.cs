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
    public class ModeratorsController : Controller
    {
        private readonly GoTravelContext _context;
        private Moderator loggedMod;

        public ModeratorsController(GoTravelContext context)
        {
            _context = context;
        }

        // GET: Moderators
        public IActionResult Index(string msg)
        {
            ViewData["msg"] = msg;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("ModeratorId,UserName,Password")] Moderator moderator)
        {
            var moderators = await _context.Moderators.ToListAsync();
            foreach (Moderator cur in moderators)
            {
                if (cur.UserName == moderator.UserName && cur.Password == moderator.Password)
                {
                    loggedMod = cur;
                    break;
                }
            }
            if (loggedMod == null)
                return RedirectToAction("Index", new { msg = "Login Credentials are incorrect" });
            return RedirectToAction("ModeratorHomePage");
        }

        // GET: CustomerBookings
        public IActionResult ModeratorHomePage()
        {
            return View();
        }

        // GET: Moderators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moderator = await _context.Moderators
                .FirstOrDefaultAsync(m => m.ModeratorId == id);
            if (moderator == null)
            {
                return NotFound();
            }

            return View(moderator);
        }

        // GET: Moderators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moderators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModeratorId,UserName,Password")] Moderator moderator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moderator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moderator);
        }

        // GET: Moderators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moderator = await _context.Moderators.FindAsync(id);
            if (moderator == null)
            {
                return NotFound();
            }
            return View(moderator);
        }

        // POST: Moderators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModeratorId,UserName,Password")] Moderator moderator)
        {
            if (id != moderator.ModeratorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moderator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeratorExists(moderator.ModeratorId))
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
            return View(moderator);
        }

        // GET: Moderators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moderator = await _context.Moderators
                .FirstOrDefaultAsync(m => m.ModeratorId == id);
            if (moderator == null)
            {
                return NotFound();
            }

            return View(moderator);
        }

        // POST: Moderators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moderator = await _context.Moderators.FindAsync(id);
            _context.Moderators.Remove(moderator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeratorExists(int id)
        {
            return _context.Moderators.Any(e => e.ModeratorId == id);
        }
    }
}
