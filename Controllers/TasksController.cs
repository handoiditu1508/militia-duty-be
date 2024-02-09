using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilitiaDuty.Data;
using MilitiaDuty.Models.Dtos;

namespace MilitiaDuty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly MilitiaContext _context;
        private readonly IMapper _mapper;

        public TasksController(MilitiaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TaskDto>>(tasks));
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(uint id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return _mapper.Map<TaskDto>(task);
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(uint id, TaskDto taskDto)
        {
            var task = _mapper.Map<Models.Assignments.Task>(taskDto);

            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
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

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskDto>> PostTask(TaskDto taskDto)
        {
            var task = _mapper.Map<Models.Assignments.Task>(taskDto);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, _mapper.Map<TaskDto>(task));
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(uint id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(uint id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
