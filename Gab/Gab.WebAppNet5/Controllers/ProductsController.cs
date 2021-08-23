using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gab.WebAppNet5.Data;
using Gab.WebAppNet5.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gab.WebAppNet5.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context) =>
            _context = context;

        // Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products
                .Include(p => p.Vendor)
                .ToListAsync());
        }

        // Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // Create
        public IActionResult Create()
        {
            ViewBag.Vendors = new SelectList(
                items: _context.Vendors,
                dataValueField: "Id",
                dataTextField: "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Vendor")] Product product)
        {
            if (ModelState.IsValid is false)
                return View(product);

            product.Vendor = await _context.Vendors
                .FirstOrDefaultAsync(v => v.Id == product.Vendor.Id);
            product.Id = Guid.NewGuid();

            _context.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Edit
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            ViewBag.Vendors = new SelectList(
                items: _context.Vendors,
                dataValueField: "Id",
                dataTextField: "Name",
                selectedValue:product.Vendor.Id);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Vendor")] Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (ModelState.IsValid is false)
                return View(product);

            try
            {
                product.Vendor = await _context.Vendors
                    .FirstOrDefaultAsync(v => v.Id == product.Vendor.Id);

                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Products.Any(p => p.Id == product.Id) is false)
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

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
