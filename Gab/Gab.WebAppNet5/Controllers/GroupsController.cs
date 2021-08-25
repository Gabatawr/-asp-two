using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gab.WebAppNet5.Data;
using Gab.WebAppNet5.Entities.School;

namespace Gab.WebAppNet5.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context) =>
            _context = context;

        // Index
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Groups
                .Include(g => g.Teacher);

            return View(await applicationDbContext.ToListAsync());
        }

        // Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var group = await _context.Groups
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (group == null)
                return NotFound();

            return View(group);
        }

        // Create
        public IActionResult Create()
        {
            ViewData["TeacherId"] = new SelectList(
                items: _context.Teachers
                    .Where(t => t.Group == null),
                dataValueField: "Id",
                dataTextField: "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TeacherId")] Group group)
        {
            if (ModelState.IsValid)
            {
                group.Id = Guid.NewGuid();

                _context.Add(group);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherId"] = new SelectList(
                items: _context.Teachers,
                dataValueField: "Id",
                dataTextField: "Name",
                selectedValue: group.TeacherId);

            return View(group);
        }

        // Edit
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var group = await _context.Groups.FindAsync(id);

            if (group == null)
                return NotFound();

            ViewData["TeacherId"] = new SelectList(
                items: _context.Teachers,
                dataValueField: "Id",
                dataTextField: "Name",
                selectedValue: group.TeacherId);

            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,TeacherId")] Group group)
        {
            if (id != group.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Groups.Any(e => e.Id == group.Id) is false)
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherId"] = new SelectList(
                items: _context.Teachers,
                dataValueField: "Id",
                dataTextField: "Name",
                selectedValue: group.TeacherId);

            return View(group);
        }

        // Delete
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var group = await _context.Groups
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (group == null)
                return NotFound();

            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var group = await _context.Groups.FindAsync(id);

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
