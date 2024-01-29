﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitiaDuty.Data;
using MilitiaDuty.Models.Rules;

namespace MilitiaDuty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly MilitiaContext _context;

        public RulesController(MilitiaContext context)
        {
            _context = context;
        }

        // GET: api/Rules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rule>>> GetRules()
        {
            return await _context.Rules.ToListAsync();
        }

        // GET: api/Rules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rule>> GetRule(uint id)
        {
            var rule = await _context.Rules.FindAsync(id);

            if (rule == null)
            {
                return NotFound();
            }

            return rule;
        }

        // PUT: api/Rules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRule(uint id, Rule rule)
        {
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
        public async Task<ActionResult<Rule>> PostRule(Rule rule)
        {
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
