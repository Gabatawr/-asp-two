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
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PostsController(ApplicationDbContext context) =>
            _context = context;

        // Index
        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts
                .Include(p => p.Category)
                .ToListAsync();

            return View(posts);
        }

        // Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        // Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(
                items:_context.Categories,
                dataValueField: "Id",
                dataTextField: "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Slug,Category")] Post post)
        {
            if (ModelState.IsValid is false) 
                return View(post);

            post.Category = _context.Categories.FirstOrDefault(c => c.Id == post.Category.Id);
            post.Id = Guid.NewGuid();

            _context.Add(post);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Edit
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return NotFound();


            ViewBag.Categories = new SelectList(
                items: _context.Categories,
                dataValueField: "Id",
                dataTextField: "Name",
                selectedValue: post.Category.Id);

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Slug,Category")] Post post)
        {
            if (id != post.Id)
                return NotFound();

            if (ModelState.IsValid is false)
                return View(post);

            try
            {
                post.Category = _context.Categories
                    .FirstOrDefault(c => c.Id == post.Category.Id);

                _context.Update(post);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Posts.Any(p => p.Id == post.Id) is false)
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

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
