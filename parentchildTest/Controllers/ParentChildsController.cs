using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using parentchildTest.Models;

namespace parentchildTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentChildsController : ControllerBase
    {
        private readonly SampleDbtestContext _context;

        public ParentChildsController(SampleDbtestContext context)
        {
            _context = context;
        }

        // GET: api/ParentChilds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentChild>>> GetParentChildren()
        {
          if (_context.ParentChildren == null)
          {
              return NotFound();
          }
            return await _context.ParentChildren.ToListAsync();
        }

        // GET: api/ParentChilds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParentChild>> GetParentChild(int id)
        {
          if (_context.ParentChildren == null)
          {
              return NotFound();
          }
            var parentChild = await _context.ParentChildren.FindAsync(id);

            if (parentChild == null)
            {
                return NotFound();
            }

            return parentChild;
        }

        // PUT: api/ParentChilds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParentChild(int id, ParentChild parentChild)
        {
            if (id != parentChild.Id)
            {
                return BadRequest();
            }

            _context.Entry(parentChild).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentChildExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ParentChilds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParentChild>> PostParentChild(ParentChild parentChild)
        {
          if (_context.ParentChildren == null)
          {
              return Problem("Entity set 'SampleDbtestContext.ParentChildren'  is null.");
          }
            _context.ParentChildren.Add(parentChild);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParentChildExists(parentChild.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetParentChild", new { id = parentChild.Id }, parentChild);
        }

        // DELETE: api/ParentChilds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParentChild(int id)
        {
            if (_context.ParentChildren == null)
            {
                return NotFound();
            }
            var parentChild = await _context.ParentChildren.FindAsync(id);
            if (parentChild == null)
            {
                return NotFound();
            }

            _context.ParentChildren.Remove(parentChild);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParentChildExists(int id)
        {
            return (_context.ParentChildren?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
