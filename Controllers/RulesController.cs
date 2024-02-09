using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitiaDuty.Data;
using MilitiaDuty.Models.Dtos;
using MilitiaDuty.Models.Rules;

namespace MilitiaDuty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly MilitiaContext _context;
        private readonly IMapper _mapper;

        public RulesController(MilitiaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Rules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleDto>>> GetRules()
        {
            var rules = await _context.Rules
                .Include(r => r.Militias)
                .Include(r => r.Tasks)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<RuleDto>>(rules));
        }

        // GET: api/Rules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RuleDto>> GetRule(uint id)
        {
            var rule = await _context.Rules
                .Include(r => r.Militias)
                .Include(r => r.Tasks)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rule == null)
            {
                return NotFound();
            }

            return _mapper.Map<RuleDto>(rule);
        }

        // PUT: api/Rules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRule(uint id, RuleDto ruleDto)
        {
            var rule = _mapper.Map<Rule>(ruleDto);

            if (id != rule.Id)
            {
                return BadRequest();
            }

            _context.Entry(rule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleExists(id))
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

        // POST: api/Rules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RuleDto>> PostRule(RuleDto ruleDto)
        {
            var rule = _mapper.Map<Rule>(ruleDto);

            _context.Rules.Add(rule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRule), new { id = rule.Id }, rule);
        }

        // DELETE: api/Rules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRule(uint id)
        {
            var rule = await _context.Rules.FindAsync(id);
            if (rule == null)
            {
                return NotFound();
            }

            _context.Rules.Remove(rule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RuleExists(uint id)
        {
            return _context.Rules.Any(e => e.Id == id);
        }
    }
}
