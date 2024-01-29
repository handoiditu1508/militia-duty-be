using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitiaDuty.Data;
using MilitiaDuty.Models.DutyDates;

namespace MilitiaDuty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DutyDatesController : ControllerBase
    {
        private readonly MilitiaContext _context;

        public DutyDatesController(MilitiaContext context)
        {
            _context = context;
        }

        // GET: api/DutyDates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DutyDate>>> GetDutyDates()
        {
            return await _context.DutyDates.ToListAsync();
        }

        // GET: api/DutyDates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DutyDate>> GetDutyDate(string id)
        {
            var dutyDate = await _context.DutyDates.FindAsync(id);

            if (dutyDate == null)
            {
                return NotFound();
            }

            return dutyDate;
        }

        // PUT: api/DutyDates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDutyDate(string id, DutyDate dutyDate)
        {
            if (id != dutyDate.Id)
            {
                return BadRequest();
            }

            _context.Entry(dutyDate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DutyDateExists(id))
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

        // POST: api/DutyDates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DutyDate>> PostDutyDate(DutyDate dutyDate)
        {
            _context.DutyDates.Add(dutyDate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DutyDateExists(dutyDate.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetDutyDate), new { id = dutyDate.Id }, dutyDate);
        }

        // DELETE: api/DutyDates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDutyDate(string id)
        {
            var dutyDate = await _context.DutyDates.FindAsync(id);
            if (dutyDate == null)
            {
                return NotFound();
            }

            _context.DutyDates.Remove(dutyDate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DutyDateExists(string id)
        {
            return _context.DutyDates.Any(e => e.Id == id);
        }
    }
}
