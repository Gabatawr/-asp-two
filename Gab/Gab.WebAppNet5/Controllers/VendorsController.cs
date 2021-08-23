using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gab.WebAppNet5.Data;
using Gab.WebAppNet5.Entities;

namespace Gab.WebAppNet5.Controllers
{
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VendorsController(ApplicationDbContext context) =>
            _context = context;

        // Index
        public async Task<IActionResult> Index() =>
            View(await _context.Vendors.ToListAsync());

        // Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vendor == null)
                return NotFound();

            return View(vendor);
        }

        // Create
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Vendor vendor)
        {
            if (ModelState.IsValid is false)
                return View(vendor);

            vendor.Id = Guid.NewGuid();

            _context.Add(vendor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Edit
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
                return NotFound();

            return View(vendor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Vendor vendor)
        {
            if (id != vendor.Id)
                return NotFound();

            if (ModelState.IsValid is false)
                return View(vendor);

            try
            {
                _context.Update(vendor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Vendors.Any(v => v.Id == vendor.Id) is false)
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

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vendor == null)
                return NotFound();

            return View(vendor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
