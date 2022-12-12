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
    public class ModRequestsController : Controller
    {
        private readonly GoTravelContext _context;

        public ModRequestsController(GoTravelContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Shows all moderator requests
        /// </summary>
        /// <returns>Opens page with moderator requests</returns>
        public async Task<IActionResult> Index()
        {
            var goTravelContext = _context.ModRequests.Include(m => m.Moderator);
            return View(await goTravelContext.ToListAsync());
        }

        /// <summary>
        /// Returns admin to their home page
        /// </summary>
        /// <param name="id">admin id</param>
        /// <returns>admin home page</returns>
        public ActionResult AdminBack(int? id)
        {
            return RedirectToAction("AdminHomePage", "Administrators", new { id = id });
        }


        /// <summary>
        /// Opens page for moderators to search moderator requests for admin
        /// </summary>
        /// <param name="id">logged in admins id</param>
        /// <param name="statusFilter">status parameter for search filtering</param>
        /// <returns>Opens page with all matching moderator requests</returns>
        public async Task<IActionResult> AdminOpen(int? id, string statusFilter)
        {
            var goTravelContext = _context.ModRequests.Include(m => m.Moderator);
            var requests = await goTravelContext.ToListAsync();
            foreach (ModRequest request in await goTravelContext.ToListAsync())
            {
                if (statusFilter != null && statusFilter != "")
                    if (statusFilter != request.Status)
                        requests.Remove(request);
            }
            ViewData["loggedAdminId"] = id;
            return View(requests);
        }

        /// <summary>
        /// Open page to Create a adminresponse 
        /// </summary>
        /// <param name="id">logged in admin id</param>
        /// <param name="requestId"> selected moderator request id</param>
        /// <returns>Opens admin respond page</returns>
        public async Task<IActionResult> AdminRespond(int? id, int requestId)
        {
            var modRequest = await _context.ModRequests.FindAsync(requestId);
            if (modRequest == null)
            {
                return NotFound();
            }
            ViewData["loggedAdminId"] = id;
            ViewData["requestId"] = requestId;
            ViewData["modId"] = modRequest.ModeratorId;
            return View();
        }

        /// <summary>
        /// Creates admin response and changes status of request to In Progress
        /// </summary>
        /// <param name="id"><logged in admin id/param>
        /// <param name="requestId">selected moderator request id</param>
        /// <param name="adminRequest">adminresponse to be made</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdminResponse(int? id, int? requestId, [Bind("ResponseId,Title,Description,ResponseTime,ModeratorId,AdminId")] AdminResponse adminRequest)
        {
            if (ModelState.IsValid)
            {
                var request = await _context.ModRequests.FindAsync(requestId);
                request.Status = "In Progress";
                _context.Update(request);
                _context.Add(adminRequest);
                await _context.SaveChangesAsync();
                ViewData["loggedAdminId"] = id;
                return RedirectToAction("AdminOpen", new { id = id });
            }
            ViewData["loggedAdminId"] = id;
            return View(adminRequest);
        }

        public ActionResult ModBack(int? id)
        {
            return RedirectToAction("ModeratorHomePage", "Moderators", new { id = id });
        }

        /// <summary>
        /// Opens page for moderators to search moderator requests for moderator
        /// </summary>
        /// <param name="id">logged in moderator id</param>
        /// <param name="statusFilter">status parameter for search filtering</param>
        /// <returns>Opens page with all matching moderator requests</returns>
        public async Task<IActionResult> ModOpen(int? id)
        {
            var goTravelContext = _context.ModRequests.Include(m => m.Moderator);
            ViewData["loggedModId"] = id;
            return View(await goTravelContext.ToListAsync());
        }

        /// <summary>
        /// Opens page to create mod request
        /// </summary>
        /// <param name="id">logged in moderator id</param>
        /// <returns>Opens page to create moderator request</returns>
        public IActionResult ModCreate(int? id)
        {
            ViewData["loggedModId"] = id;
            return View();
        }

        /// <summary>
        /// Creates moderator request
        /// </summary>
        /// <param name="modRequest">mod request to be made</param>
        /// <returns>Creates mod request</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoModCreate([Bind("RequestId,Title,Description,Status,RequestTime,ModeratorId")] ModRequest modRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modRequest);
                await _context.SaveChangesAsync();
                ViewData["loggedModId"] = modRequest.ModeratorId;
                return RedirectToAction("ModOpen", new { id = modRequest.ModeratorId });
            }
            ViewData["loggedModId"] = modRequest.ModeratorId;
            ViewData["ModeratorId"] = new SelectList(_context.Moderators, "ModeratorId", "Password", modRequest.ModeratorId);
            return View(modRequest);
        }

        /// <summary>
        /// Opens page to edit mod request
        /// </summary>
        /// <param name="id">logged in mods id</param>
        /// <param name="requestId">request to be updated id</param>
        /// <returns>page to edit request</returns>
        public async Task<IActionResult> ModEdit(int? id, int? requestId)
        {
            var modRequest = await _context.ModRequests.FindAsync(requestId);
            if (modRequest == null)
            {
                return NotFound();
            }
            ViewData["loggedModId"] = id;
            return View(modRequest);
        }

        /// <summary>
        /// Edits mod request
        /// </summary>
        /// <param name="id">logged in mods id</param>
        /// <param name="modRequest">mod request to be updated</param>
        /// <returns>Edits existing mod request</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoModEdit(int id, [Bind("RequestId,Title,Description,Status,RequestTime,ModeratorId")] ModRequest modRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModRequestExists(modRequest.RequestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["loggedModId"] = id;
                return RedirectToAction("ModOpen", new { id = modRequest.ModeratorId });
            }
            ViewData["loggedModId"] = id;
            ViewData["ModeratorId"] = new SelectList(_context.Moderators, "ModeratorId", "Password", modRequest.ModeratorId);
            return View(modRequest);
        }

        // GET: ModRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modRequest = await _context.ModRequests
                .Include(m => m.Moderator)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (modRequest == null)
            {
                return NotFound();
            }

            return View(modRequest);
        }

        // GET: ModRequests/Create
        public IActionResult Create()
        {
            ViewData["ModeratorId"] = new SelectList(_context.Moderators, "ModeratorId", "Password");
            return View();
        }

        // POST: ModRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,Title,Description,Status,RequestTime,ModeratorId")] ModRequest modRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeratorId"] = new SelectList(_context.Moderators, "ModeratorId", "Password", modRequest.ModeratorId);
            return View(modRequest);
        }

        // GET: ModRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modRequest = await _context.ModRequests.FindAsync(id);
            if (modRequest == null)
            {
                return NotFound();
            }
            ViewData["ModeratorId"] = new SelectList(_context.Moderators, "ModeratorId", "Password", modRequest.ModeratorId);
            return View(modRequest);
        }

        // POST: ModRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,Title,Description,Status,RequestTime,ModeratorId")] ModRequest modRequest)
        {
            if (id != modRequest.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModRequestExists(modRequest.RequestId))
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
            ViewData["ModeratorId"] = new SelectList(_context.Moderators, "ModeratorId", "Password", modRequest.ModeratorId);
            return View(modRequest);
        }

        // GET: ModRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modRequest = await _context.ModRequests
                .Include(m => m.Moderator)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (modRequest == null)
            {
                return NotFound();
            }

            return View(modRequest);
        }

        // POST: ModRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modRequest = await _context.ModRequests.FindAsync(id);
            _context.ModRequests.Remove(modRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModRequestExists(int id)
        {
            return _context.ModRequests.Any(e => e.RequestId == id);
        }
    }
}
