using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitiaDuty.Data;
using MilitiaDuty.Models.Dtos;
using MilitiaDuty.Models.DutyDates;

namespace MilitiaDuty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly MilitiaContext _context;
        private readonly IMapper _mapper;

        public ShiftsController(MilitiaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Shifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetShift()
        {
            var shifts = await _context.Shifts
                .Include(s => s.Militia)
                .Include(s => s.DutyDate)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ShiftDto>>(shifts));
        }

        // GET: api/Shifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftDto>> GetShift(ulong id)
        {
            var shift = await _context.Shifts
                .Include(s => s.Militia)
                .Include(s => s.DutyDate)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (shift == null)
            {
                return NotFound();
            }

            return _mapper.Map<ShiftDto>(shift);
        }

        // PUT: api/Shifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShift(ulong id, TaskDto shiftDto)
        {
            var shift = _mapper.Map<Shift>(shiftDto);

            if (id != shift.Id)
            {
                return BadRequest();
            }

            _context.Entry(shift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftExists(id))
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

        // POST: api/Shifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftDto>> PostShift(ShiftDto shiftDto)
        {
            var shift = _mapper.Map<Shift>(shiftDto);

            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShift), new { id = shift.Id }, _mapper.Map<ShiftDto>(shift));
        }

        // DELETE: api/Shifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(ulong id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftExists(ulong id)
        {
            return _context.Shifts.Any(e => e.Id == id);
        }
    }
}
