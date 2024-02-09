using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitiaDuty.Data;
using MilitiaDuty.Models.Dtos;
using MilitiaDuty.Models.DutyDates;
using MilitiaDuty.Models.Filters;

namespace MilitiaDuty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DutyDatesController : ControllerBase
    {
        private readonly MilitiaContext _context;
        private readonly IMapper _mapper;

        public DutyDatesController(MilitiaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/DutyDates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DutyDateDto>>> GetDutyDates([FromQuery] DutyDateFilter filter)
        {
            IQueryable<DutyDate> dutyDates = _context.DutyDates
                .Include(d => d.Militias)
                .Include(d => d.Shifts);

            if (filter.StartDate != null)
            {
                dutyDates = dutyDates.Where(d => d.Date >= filter.StartDate);
            }

            if (filter.EndDate != null)
            {
                dutyDates = dutyDates.Where(d => d.Date <= filter.EndDate);
            }

            var dutyDatesList = await dutyDates.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<DutyDateDto>>(dutyDatesList));
        }

        // GET: api/DutyDates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DutyDateDto>> GetDutyDate(string id)
        {
            var dutyDate = await _context.DutyDates
                .Include(d => d.Militias)
                .Include(d => d.Shifts)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dutyDate == null)
            {
                return NotFound();
            }

            return _mapper.Map<DutyDateDto>(dutyDate);
        }

        // PUT: api/DutyDates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDutyDate(string id, DutyDateDto dutyDateDto)
        {
            var dutyDate = _mapper.Map<DutyDate>(dutyDateDto);

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
        public async Task<ActionResult<DutyDateDto>> PostDutyDate(DutyDate dutyDateDto)
        {
            var dutyDate = _mapper.Map<DutyDate>(dutyDateDto);

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

            return CreatedAtAction(nameof(GetDutyDate), new { id = dutyDate.Id }, _mapper.Map<DutyDateDto>(dutyDate));
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
