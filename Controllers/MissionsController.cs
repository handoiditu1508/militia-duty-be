using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitiaDuty.Data;
using MilitiaDuty.Models.Assignments;
using MilitiaDuty.Models.Dtos;

namespace MilitiaDuty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly MilitiaContext _context;
        private readonly IMapper _mapper;

        public MissionsController(MilitiaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Missions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionDto>>> GetMissions()
        {
            var missions = await _context.Missions
                .Include(m => m.Tasks)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<MissionDto>>(missions));
        }

        // GET: api/Missions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MissionDto>> GetMission(uint id)
        {
            var mission = await _context.Missions
                .Include(m => m.Tasks)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mission == null)
            {
                return NotFound();
            }

            return _mapper.Map<MissionDto>(mission);
        }

        // PUT: api/Missions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMission(uint id, MissionDto missionDto)
        {
            var mission = _mapper.Map<Mission>(missionDto);

            if (id != mission.Id)
            {
                return BadRequest();
            }

            _context.Entry(mission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionExists(id))
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

        // POST: api/Missions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MissionDto>> PostMission(MissionDto missionDto)
        {
            var mission = _mapper.Map<Mission>(missionDto);

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMission), new { id = mission.Id }, _mapper.Map<MissionDto>(mission));
        }

        // DELETE: api/Missions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(uint id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
            {
                return NotFound();
            }

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MissionExists(uint id)
        {
            return _context.Missions.Any(e => e.Id == id);
        }
    }
}
