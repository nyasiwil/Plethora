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
    public class ArticlesController : Controller
    {
        private readonly PlethoraDbContext _context;

        public ArticlesController(PlethoraDbContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var plethoraDbContext = _context.Articles.Include(a => a.ArticleType).Include(a => a.Status);
            return View(await plethoraDbContext.ToListAsync());
        }

        public async Task<IActionResult> List()
        {
            var plethoraDbContext = _context.Articles.Include(a => a.ArticleType).Include(a => a.Status);
            return View(await plethoraDbContext.ToListAsync());

        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles
                .Include(a => a.ArticleType)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (articles == null)
            {
                return NotFound();
            }

            return View(articles);
        }

        // GET: Articles/Info/5
        public async Task<IActionResult> Info(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles
                .Include(a => a.ArticleType)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (articles == null)
            {
                return NotFound();
            }

            return View(articles);
        }
        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["ArticleTypeId"] = new SelectList(_context.ArticleTypes, "ArticleTypeId", "Title");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Title");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,ArticleTypeId,Title,Intro,ByLine,BodyCopy,StatusId,DateUpdated,DateCreated")] Articles articles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleTypeId"] = new SelectList(_context.ArticleTypes, "ArticleTypeId", "Title", articles.ArticleTypeId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Title", articles.StatusId);
            return View(articles);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles.FindAsync(id);
            if (articles == null)
            {
                return NotFound();
            }
            ViewData["ArticleTypeId"] = new SelectList(_context.ArticleTypes, "ArticleTypeId", "Title", articles.ArticleTypeId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Title", articles.StatusId);
            return View(articles);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,ArticleTypeId,Title,Intro,ByLine,BodyCopy,StatusId,DateUpdated,DateCreated")] Articles articles)
        {
            if (id != articles.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticlesExists(articles.ArticleId))
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
            ViewData["ArticleTypeId"] = new SelectList(_context.ArticleTypes, "ArticleTypeId", "Title", articles.ArticleTypeId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Title", articles.StatusId);
            return View(articles);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles
                .Include(a => a.ArticleType)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (articles == null)
            {
                return NotFound();
            }

            return View(articles);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articles = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(articles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticlesExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }
    }
}
