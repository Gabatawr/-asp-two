using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gab.WebAppNet5.Data;
using Gab.WebAppNet5.Entities;

namespace Gab.WebAppNet5.Controllers
{
    public class CatalogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CatalogsController(ApplicationDbContext context) => 
            _context = context;

        // Index
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Catalog
                .Include(c => c.Parent);

            return View(await applicationDbContext.ToListAsync());
        }

        // Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var catalog = await _context.Catalog
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (catalog == null)
                return NotFound();

            return View(catalog);
        }

        // Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(
                items: _context.Catalog,
                dataValueField: "Id",
                dataTextField: "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ParentId")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                catalog.Id = Guid.NewGuid();

                _context.Add(catalog);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ParentId"] = new SelectList(
                items: _context.Catalog,
                dataValueField: "Id",
                dataTextField: "Name",
                catalog.ParentId);

            return View(catalog);
        }

        // Edit
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var catalog = await _context.Catalog.FindAsync(id);

            if (catalog == null)
                return NotFound();

            ViewData["ParentId"] = new SelectList(
                items: _context.Catalog,
                dataValueField: "Id",
                dataTextField: "Name",
                catalog.ParentId);

            return View(catalog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ParentId")] Catalog catalog)
        {
            if (id != catalog.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Catalog.Any(e => e.Id == catalog.Id) is false)
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(
                items: _context.Catalog,
                dataValueField: "Id",
                dataTextField: "Name",
                catalog.ParentId);

            return View(catalog);
        }

        // Delete
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var catalog = await _context.Catalog
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (catalog == null)
                return NotFound();

            return View(catalog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var catalog = await _context.Catalog.FindAsync(id);

            _context.Catalog.Remove(catalog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
