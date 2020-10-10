using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plethora.Models;

namespace Plethora.Controllers
{
    public class ArticleTypesController : Controller
    {
        private readonly PlethoraDbContext _context;

        public ArticleTypesController(PlethoraDbContext context)
        {
            _context = context;
        }

        // GET: ArticleTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArticleTypes.ToListAsync());
        }

        // GET: ArticleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleTypes = await _context.ArticleTypes
                .FirstOrDefaultAsync(m => m.ArticleTypeId == id);
            if (articleTypes == null)
            {
                return NotFound();
            }

            return View(articleTypes);
        }

        // GET: ArticleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArticleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleTypeId,Title,DateCreated")] ArticleTypes articleTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articleTypes);
        }

        // GET: ArticleTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleTypes = await _context.ArticleTypes.FindAsync(id);
            if (articleTypes == null)
            {
                return NotFound();
            }
            return View(articleTypes);
        }

        // POST: ArticleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleTypeId,Title,DateCreated")] ArticleTypes articleTypes)
        {
            if (id != articleTypes.ArticleTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleTypesExists(articleTypes.ArticleTypeId))
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
            return View(articleTypes);
        }

        // GET: ArticleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleTypes = await _context.ArticleTypes
                .FirstOrDefaultAsync(m => m.ArticleTypeId == id);
            if (articleTypes == null)
            {
                return NotFound();
            }

            return View(articleTypes);
        }

        // POST: ArticleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articleTypes = await _context.ArticleTypes.FindAsync(id);
            _context.ArticleTypes.Remove(articleTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleTypesExists(int id)
        {
            return _context.ArticleTypes.Any(e => e.ArticleTypeId == id);
        }
    }
}
