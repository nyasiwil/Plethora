using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plethora.Models;

namespace Plethora.Controllers
{
    public class HighlightsController : Controller
    {
        private readonly PlethoraDbContext _context;

        public HighlightsController(PlethoraDbContext context)
        {
            _context = context;
        }

        // GET: Highlights
        public async Task<IActionResult> Index()
        {
            var plethoraDbContext = _context.Highlights.Include(h => h.Status);
            return View(await plethoraDbContext.ToListAsync());
        }

        // GET: Highlights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlights = await _context.Highlights
                .Include(h => h.Status)
                .FirstOrDefaultAsync(m => m.HighlightId == id);
            if (highlights == null)
            {
                return NotFound();
            }

            return View(highlights);
        }

        // GET: Highlights/Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Title");
            return View();
        }

        // POST: Highlights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HighlightId,Title,Description,NewTab,StatusId,DateUpdated,DateCreated")] Highlights highlights)
        {
            if (ModelState.IsValid)
            {
                _context.Add(highlights);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Title", highlights.StatusId);
            return View(highlights);
        }

        //[HttpPost]
        //public ActionResult Upload(HttpPostedFileBase file)
        //{
        //    if (file != null && file.ContentLength > 0)
        //        try
        //        {
        //            string path = Path.Combine(Server.MapPath("~/Images"),
        //                                       Path.GetFileName(file.FileName));
        //            file.SaveAs(path);
        //            ViewBag.Message = "File uploaded successfully";
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Message = "ERROR:" + ex.Message.ToString(
        
        // GET: Highlights/Edit/5
                    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlights = await _context.Highlights.FindAsync(id);
            if (highlights == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Title", highlights.StatusId);
            return View(highlights);
        }

        // POST: Highlights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HighlightId,Title,Description,NewTab,StatusId,DateUpdated,DateCreated")] Highlights highlights)
        {
            if (id != highlights.HighlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(highlights);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HighlightsExists(highlights.HighlightId))
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
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Title", highlights.StatusId);
            return View(highlights);
        }

        // GET: Highlights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highlights = await _context.Highlights
                .Include(h => h.Status)
                .FirstOrDefaultAsync(m => m.HighlightId == id);
            if (highlights == null)
            {
                return NotFound();
            }

            return View(highlights);
        }

        // POST: Highlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var highlights = await _context.Highlights.FindAsync(id);
            _context.Highlights.Remove(highlights);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HighlightsExists(int id)
        {
            return _context.Highlights.Any(e => e.HighlightId == id);
        }
    }
}
