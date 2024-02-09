using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitiaDuty.Data;
using MilitiaDuty.Models.Dtos;
using MilitiaDuty.Models.Militias;

namespace MilitiaDuty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilitiasController : ControllerBase
    {
        private readonly MilitiaContext _context;
        private readonly IMapper _mapper;

        public MilitiasController(MilitiaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Militias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MilitiaDto>>> GetMilitias()
        {
            var militias = await _context.Militias.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<MilitiaDto>>(militias));
        }

        // GET: api/Militias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MilitiaDto>> GetMilitia(uint id)
        {
            var militia = await _context.Militias.FindAsync(id);

            if (militia == null)
            {
                return NotFound();
            }

            return _mapper.Map<MilitiaDto>(militia);
        }

        // PUT: api/Militias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMilitia(uint id, MilitiaDto militiaDto)
        {
            var militia = _mapper.Map<Militia>(militiaDto);

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
        public async Task<ActionResult<MilitiaDto>> PostMilitia(MilitiaDto militiaDto)
        {
            var militia = _mapper.Map<Militia>(militiaDto);

            _context.Militias.Add(militia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMilitia), new { id = militia.Id }, _mapper.Map<MilitiaDto>(militia));
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
