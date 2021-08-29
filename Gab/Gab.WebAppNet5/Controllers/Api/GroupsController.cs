using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gab.WebAppNet5.Data;
using Gab.WebAppNet5.Entities.School;

namespace Gab.WebAppNet5.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GroupsController(ApplicationDbContext context) =>
            _context = context;

        // GET: Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups() =>
            await _context.Groups
                .Include(g => g.Students)
                .Include(g => g.Teacher)
                .ToListAsync();

        // GET: Group
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(Guid id)
        {
            var group = await _context.Groups.FindAsync(id);

            if (group == null)
                return NotFound();

            return group;
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(Guid id, Group group)
        {
            if (id != group.Id || _context.Teachers.Any(t => t.Id == group.TeacherId) is false)
                return BadRequest();

            _context.Entry(group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Groups.Any(e => e.Id == id) is false)
                    return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup([FromBody] Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new
            {
                id = group.Id
            }, group);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            var group = await _context.Groups.FindAsync(id);

            if (group == null)
                return NotFound();

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
