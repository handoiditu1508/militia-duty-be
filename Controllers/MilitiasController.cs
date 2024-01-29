using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitiaDuty.Data;
using MilitiaDuty.Models.Militias;

namespace MilitiaDuty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilitiasController : ControllerBase
    {
        private readonly MilitiaContext _context;

        public MilitiasController(MilitiaContext context)
        {
            _context = context;
        }

        // GET: api/Militias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Militia>>> GetMilitias()
        {
            return await _context.Militias.ToListAsync();
        }

        // GET: api/Militias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Militia>> GetMilitia(uint id)
        {
            var militia = await _context.Militias.FindAsync(id);

            if (militia == null)
            {
                return NotFound();
            }

            return militia;
        }

        // PUT: api/Militias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMilitia(uint id, Militia militia)
        {
            if (id != militia.Id)
            {
                return BadRequest();
            }

            _context.Entry(militia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MilitiaExists(id))
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

        // POST: api/Militias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Militia>> PostMilitia(Militia militia)
        {
            _context.Militias.Add(militia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMilitia), new { id = militia.Id }, militia);
        }

        // DELETE: api/Militias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMilitia(uint id)
        {
            var militia = await _context.Militias.FindAsync(id);
            if (militia == null)
            {
                return NotFound();
            }

            _context.Militias.Remove(militia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MilitiaExists(uint id)
        {
            return _context.Militias.Any(e => e.Id == id);
        }
    }
}
