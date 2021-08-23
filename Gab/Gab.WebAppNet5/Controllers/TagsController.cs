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
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TagsController(ApplicationDbContext context) =>
            _context = context;

        // Index
        public async Task<IActionResult> Index() =>
            View(await _context.Tags.ToListAsync());

        // Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var tag = await _context.Tags
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tag == null)
                return NotFound();

            return View(tag);
        }

        // Create
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(
                items: _context.Products,
                dataValueField: "Id",
                dataTextField: "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug")] Tag tag, Guid[] Products)
        {
            if (ModelState.IsValid is false)
                return View(tag);

            tag.Id = Guid.NewGuid();
            tag.Products = _context.Products.Where(p => Products.Contains(p.Id)).ToList();

            _context.Add(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Edit
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                return NotFound();

            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Slug")] Tag tag)
        {
            if (id != tag.Id)
                return NotFound();

            if (ModelState.IsValid is false)
                return View(tag);

            try
            {
                _context.Update(tag);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Tags.Any(t => t.Id == tag.Id) is false)
                    return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // Delete
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tag == null)
                return NotFound();

            return View(tag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
