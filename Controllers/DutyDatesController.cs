using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MilitiaDuty.Data;
using MilitiaDuty.Models.Dtos;
using MilitiaDuty.Models.DutyDates;
using MilitiaDuty.Models.Filters;
using MilitiaDuty.Models.Militias;
using MilitiaDuty.Models.Options;
using MilitiaDuty.Models.Requests;
using MilitiaDuty.Models.Rules;

namespace MilitiaDuty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DutyDatesController : ControllerBase
    {
        private readonly MilitiaContext _context;
        private readonly IMapper _mapper;
        private readonly MilitiaOptions _options;

        public DutyDatesController(MilitiaContext context, IMapper mapper, IOptions<MilitiaOptions> options)
        {
            _context = context;
            _mapper = mapper;
            _options = options.Value;
        }

        // GET: api/DutyDates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DutyDateDto>>> GetDutyDates([FromQuery] DutyDateFilter filter)
        {
            IQueryable<DutyDate> dutyDates = _context.DutyDates
                .Include(d => d.Militias)
                .Include(d => d.Shifts)
                    .ThenInclude(s => s.Militia);

            if (filter.StartDate.HasValue)
            {
                filter.StartDate = filter.StartDate.Value.Date;
                dutyDates = dutyDates.Where(d => d.Date >= filter.StartDate);
            }

            if (filter.EndDate != null)
            {
                filter.EndDate = filter.EndDate.Value.Date;
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
                    .ThenInclude(s => s.Militia)
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
        /*[HttpPost]
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
        }*/

        // DELETE: api/DutyDates/5
        /*[HttpDelete("{id}")]
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
        }*/

        private bool DutyDateExists(string id)
        {
            return _context.DutyDates.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<DutyDateDto>>> Do(DoDutyDatesRequest request)
        {
            request.StartDate = request.StartDate.Date;
            request.EndDate = request.EndDate.Date;

            var activeMilitias = await _context.Militias
                .Include(m => m.Rules)
                    .ThenInclude(r => r.Tasks)
                .Where(m => m.Status == MilitiaStatus.Actice)
                .ToListAsync();
            var militiaRatesDict = activeMilitias.ToDictionary(m => m.Id, m => GetMilitiaDutyRate(m.Rules));

            await UndoAssignments(request.StartDate, request.EndDate, militiaRatesDict);

            var todayTasks = await _context.Tasks.Where(t => t.Mission!.Status == Models.Assignments.MissionStatus.Actice).ToListAsync();
            var dutyDates = new List<DutyDateDto>();
            for (var date = request.StartDate; date <= request.EndDate; date = date.AddDays(1))
            {
                var dutyDate = AssignTasks(date, activeMilitias, todayTasks, militiaRatesDict, request.IsFullDutyDate);

                dutyDates.Add(_mapper.Map<DutyDateDto>(dutyDate));
            }
            await _context.SaveChangesAsync();
            return Ok(dutyDates);
        }

        [HttpDelete]
        public async Task<IActionResult> Undo([FromQuery] UndoDutyDateRequest request)
        {
            var activeMilitias = await _context.Militias
                .Include(m => m.Rules)
                    .ThenInclude(r => r.Tasks)
                .Where(m => m.Status == MilitiaStatus.Actice)
                .ToListAsync();
            var militiaRatesDict = activeMilitias.ToDictionary(m => m.Id, m => GetMilitiaDutyRate(m.Rules));

            await UndoAssignments(request.StartDate, request.EndDate, militiaRatesDict);

            return NoContent();
        }

        #region Helper functions

        private float GetMilitiaDutyRate(IEnumerable<Rule> rules)
        {
            foreach (var rule in rules)
            {
                switch (rule.Type)
                {
                    case RuleType.WeeklyDutyOnly:
                    case RuleType.FullDuty:
                        return 0;
                    case RuleType.AlternatingDuty:
                        return 1 / (rule.Militias.Count - 1);
                }
            }

            return _options.OnDutyRate;
        }

        private bool CanMilitiaOnDuty(Militia militia, DateTime date)
        {
            date = date.Date;

            var activeRules = militia.Rules.Where(r =>
                r.StartDate == date ||
                (r.StartDate < date && !r.EndDate.HasValue) ||
                (r.StartDate < date && r.EndDate.HasValue && r.EndDate.Value >= date)
            );

            foreach (var rule in activeRules)
            {
                switch (rule.Type)
                {
                    case RuleType.DateOff:
                        return false;
                    case RuleType.WeeklyDutyOnly:
                        if (rule.Weekdays != null && !rule.Weekdays.Contains(date.DayOfWeek))
                        {
                            return false;
                        }
                        break;
                    case RuleType.AlternatingDuty:
                        var minScore = rule.Militias.Min(m => m.DutyDateScore);
                        if (militia.DutyDateScore > minScore)
                        {
                            return false;
                        }
                        if (militia.DutyDateScore == minScore)
                        {
                            var minScoreMilitias = rule.Militias.Where(m => m.DutyDateScore == minScore);
                            if (militia.Id != minScoreMilitias.MinBy(m => m.Id)!.Id)
                            {
                                return false;
                            }
                        }
                        break;
                    case RuleType.PreferOffDays:
                        if (militia.DutyDateScore >= _options.OnDutyRate && rule.Weekdays != null && rule.Weekdays.Contains(date.DayOfWeek))
                        {
                            return false;
                        }
                        break;
                }
            }

            return true;
        }

        private bool IsForcedDuty(Militia militia, DateTime date)
        {
            date = date.Date;

            var activeRules = militia.Rules.Where(r =>
                r.StartDate == date ||
                (r.StartDate < date && !r.EndDate.HasValue) ||
                (r.StartDate < date && r.EndDate.HasValue && r.EndDate.Value >= date)
            );

            foreach (var rule in activeRules)
            {
                switch (rule.Type)
                {
                    case RuleType.DutyDate:
                    case RuleType.FullDuty:
                        return true;
                    case RuleType.WeeklyDutyOnly:
                        if (rule.Weekdays != null && rule.Weekdays.Contains(date.DayOfWeek))
                        {
                            return true;
                        }
                        break;
                    case RuleType.AlternatingDuty:
                        var maxScore = rule.Militias.Max(m => m.DutyDateScore);
                        if (militia.DutyDateScore == maxScore)
                        {
                            var maxScoreMilitias = rule.Militias.Where(m => m.DutyDateScore == maxScore);
                            if (militia.Id == maxScoreMilitias.MinBy(m => m.Id)!.Id)
                            {
                                return true;
                            }
                        }
                        break;
                    case RuleType.PreferOffDays:
                        if (rule.Weekdays != null && (militia.DutyDateScore - _options.OnDutyRate * rule.Weekdays.Count()) < _options.OnDutyRate)
                        {
                            return true;
                        }
                        break;
                }
            }

            return false;
        }

        private DutyDate AssignTasks(DateTime date, IEnumerable<Militia> militias, IEnumerable<Models.Assignments.Task> tasks, IDictionary<uint, float> militiaRatesDict, bool isFullDutyDate = false)
        {
            tasks = tasks.OrderBy(m => Random.Shared.Next()).ToList();

            date = date.Date;
            var dutyDate = new DutyDate
            {
                Id = date.ToString("yyyyMMdd"),
                Date = date,
                IsFullDutyDate = isFullDutyDate,
            };

            IEnumerable<Militia> dutyableMilitias = isFullDutyDate ? militias : militias.Where(m => CanMilitiaOnDuty(m, date)).ToList();

            // list of militias of this dutyDate
            IEnumerable<Militia> militiasList = dutyableMilitias
                .OrderBy(m => Random.Shared.Next())// shuffle
                .OrderBy(m => m.DutyDateScore)// order by dutyDateScore
                .Take((int)_options.IdealMilitiaPerDate)// take ideal militia number
                .ToList();

            // count all shifts to assign
            var shiftCount = (int)tasks.Sum(m => m.MilitiaNumber);

            // make dictionary to count how many militias are assigned to a task
            var assignedTasksCountDict = tasks.ToDictionary(t => t.Id, t => 0);
            // make a dictionary of what tasks a militia able to handle
            var militiaTasksDict = militiasList.ToDictionary(m => m.Id, m => new List<Models.Assignments.Task>());
            // make a dictionary of how many militias can approriately assign to a task
            var taskMilitiasCountDict = tasks.ToDictionary(t => t.Id, t => 0);

            foreach (var militia in militiasList)
            {
                foreach (var task in tasks)
                {
                    var activeRules = militia.Rules.Where(r =>
                        r.StartDate == date ||
                        (r.StartDate < date && !r.EndDate.HasValue) ||
                        (r.StartDate < date && r.EndDate.HasValue && r.EndDate.Value >= date)
                    );
                    if (CanMilitiaTakeTask(militia, activeRules, task))
                    {
                        militiaTasksDict[militia.Id].Add(task);
                        taskMilitiasCountDict[task.Id]++;
                    }
                }
            }

            // order militiasList by task count
            militiasList = militiasList
                .OrderBy(m => Random.Shared.Next())// shuffle
                .OrderBy(m => m.AssignmentScore)
                .ToList();

            foreach (var militia in militiasList)
            {
                if (!isFullDutyDate)
                {
                    // put militia on that duty date
                    dutyDate.Militias.Add(militia);
                }

                // order militia's approriate tasks by how unlikely a task fit for a millitia
                militiaTasksDict[militia.Id] = militiaTasksDict[militia.Id]
                    .OrderBy(t => Random.Shared.Next())// shuffle
                    .OrderBy(t => taskMilitiasCountDict[t.Id])
                    .ToList();

                foreach (var taskToAssign in militiaTasksDict[militia.Id])
                {
                    // check task still has available shift
                    if (assignedTasksCountDict[taskToAssign.Id] < taskToAssign.MilitiaNumber)
                    {
                        // assign task to militia
                        dutyDate.Shifts.Add(new Shift
                        {
                            DutyDateId = dutyDate.Id,
                            MilitiaId = militia.Id,
                            TaskId = taskToAssign.Id,
                        });

                        assignedTasksCountDict[taskToAssign.Id]++;

                        shiftCount--;

                        break;
                    }
                }
            }

            // there is still shift to assign
            // and there is still free militia
            if (shiftCount > 0)
            {
                IEnumerable<Militia>? unassignedMilitias = null;

                if (!isFullDutyDate && dutyableMilitias.Count() > dutyDate.Militias.Count)
                {
                    unassignedMilitias = dutyableMilitias.Where(m => !dutyDate.Militias.Contains(m) && !dutyDate.Shifts.Any(s => s.MilitiaId == m.Id));
                }
                else if (isFullDutyDate && dutyableMilitias.Count() > militiasList.Count())
                {
                    unassignedMilitias = dutyableMilitias.Where(m => !militiasList.Contains(m));
                }
                else
                {
                    unassignedMilitias = [];
                }
                // get all unassigned militias
                // order militias by duty score
                unassignedMilitias = unassignedMilitias
                    .OrderBy(m => Random.Shared.Next())// shuffle
                    .OrderBy(m => m.DutyDateScore)
                    .ToList();

                foreach (var militia in unassignedMilitias)
                {
                    foreach (var task in tasks)
                    {
                        // check task still has available shift
                        if (assignedTasksCountDict[task.Id] < task.MilitiaNumber)
                        {
                            if (!isFullDutyDate)
                            {
                                // put militia on that duty date
                                dutyDate.Militias.Add(militia);
                            }

                            // assign task to militia
                            dutyDate.Shifts.Add(new Shift
                            {
                                DutyDateId = dutyDate.Id,
                                MilitiaId = militia.Id,
                                TaskId = task.Id,
                            });

                            assignedTasksCountDict[task.Id]++;

                            shiftCount--;

                            break;
                        }
                    }
                }
            }

            // there is still shift to assign
            // then one militia will take multiple shifts
            if (shiftCount > 0)
            {
                // get all militias
                // order by assignment score
                var militiasList2 = dutyableMilitias
                    .OrderBy(m => Random.Shared.Next())// shuffle
                    .OrderBy(m => m.AssignmentScore)
                    .ToList();
                int militiaIndex = 0;

                foreach (var task in tasks)
                {
                    while (assignedTasksCountDict[task.Id] < task.MilitiaNumber)
                    {
                        var militia = militiasList2[militiaIndex % militiasList2.Count];
                        militiaIndex++;

                        // assign task to militia
                        dutyDate.Shifts.Add(new Shift
                        {
                            DutyDateId = dutyDate.Id,
                            MilitiaId = militia.Id,
                            TaskId = task.Id,
                        });

                        assignedTasksCountDict[task.Id]++;

                        shiftCount--;
                    }
                }
            }

            // get forced militias
            if (!isFullDutyDate)
            {
                var forcedDutyMilitias = dutyableMilitias.Where(m => IsForcedDuty(m, date)).ToList();
                foreach (var militia in forcedDutyMilitias)
                {
                    if (!dutyDate.Militias.Contains(militia))
                    {
                        // put militia on that duty date
                        dutyDate.Militias.Add(militia);
                    }
                }
            }

            // militias on that date is lesser than ideal militia number
            // and there is still free militia
            // then add more militias
            if (!isFullDutyDate && dutyDate.Militias.Count < _options.IdealMilitiaPerDate && dutyableMilitias.Count() > dutyDate.Militias.Count)
            {
                // get all unassigned militias
                // order militias by duty score
                var unassignedMilitias = dutyableMilitias
                    .Where(m => !dutyDate.Militias.Contains(m))
                    .OrderBy(m => Random.Shared.Next())// shuffle
                    .OrderBy(m => m.DutyDateScore)
                    .ToList();

                foreach (var militia in unassignedMilitias)
                {
                    if (dutyDate.Militias.Count < _options.IdealMilitiaPerDate)
                    {
                        // put militia on that duty date
                        dutyDate.Militias.Add(militia);
                    }
                    else
                    {
                        break;
                    }
                }
            }


            foreach (var militia in militias)
            {
                if (dutyDate.Militias.Contains(militia))
                {
                    // add duty score
                    militia.DutyDateScore++;
                    if (dutyDate.Shifts.Any(s => s.MilitiaId == militia.Id))
                    {
                        // add asignment score
                        militia.AssignmentScore++;
                    }
                    else if (!militia.Rules.Any(r => r.Type == RuleType.TaskImmune))
                    {
                        // decrease assignment score for militias on duty but don't have any shift
                        militia.AssignmentScore--;
                    }
                }
                else if (!isFullDutyDate)
                {
                    // decrease duty score for militia not on duty
                    militia.DutyDateScore -= militiaRatesDict[militia.Id];
                }
            }

            _context.DutyDates.Add(dutyDate);

            return dutyDate;
        }

        private bool CanMilitiaTakeTask(Militia militia, IEnumerable<Rule> rules, Models.Assignments.Task task)
        {
            foreach (var rule in rules)
            {
                switch (rule.Type)
                {
                    case RuleType.IncludeTasks:
                        if (!rule.Tasks.Contains(task))
                        {
                            return false;
                        }
                        break;
                    case RuleType.ExcludeTasks:
                        if (rule.Tasks.Contains(task))
                        {
                            return false;
                        }
                        break;
                    case RuleType.TaskImmune:
                        return false;
                }
            }

            return true;
        }

        private async Task UndoAssignments(DateTime startDate, DateTime endDate, IDictionary<uint, float> militiaRatesDict)
        {
            // get dutyDates in range
            var existingDutyDates = await _context.DutyDates
                .Include(d => d.Militias)
                .Include(d => d.Shifts)
                    .ThenInclude(s => s.Militia)
                .Where(d => d.Date >= startDate && d.Date <= endDate)
                .ToListAsync();

            // get all active militias
            var activeMilitias = existingDutyDates.Any(d => !d.IsFullDutyDate)
                ? await _context.Militias
                    .Where(m => m.Status == MilitiaStatus.Actice)
                    .ToListAsync()
                : [];

            foreach (var dutyDate in existingDutyDates)
            {
                if (!dutyDate.IsFullDutyDate)
                {
                    // make dictionary of all active militias
                    var militiasDict2 = activeMilitias.ToDictionary(m => m.Id, m => m);

                    // decrease score for militias that suppose to be on duty that date
                    foreach (var militia in dutyDate.Militias)
                    {
                        militia.DutyDateScore--;

                        militiasDict2.Remove(militia.Id);
                    }

                    // readd score for militias that suppose to off that date
                    foreach (var militia in militiasDict2.Values)
                    {
                        militia.DutyDateScore += militiaRatesDict[militia.Id];
                    }
                }

                // make dictionary of all militias on duty that date
                var militiasDict = dutyDate.Militias.ToDictionary(m => m.Id, m => m);

                // decrease score for militias that have shift on that date
                foreach (var shift in dutyDate.Shifts)
                {
                    shift.Militia!.AssignmentScore--;

                    militiasDict.Remove(shift.Militia.Id);
                }

                // readd score for militias that on duty that date but not have shift
                foreach (var militia in militiasDict.Values)
                {
                    if (!militia.Rules.Any(r => r.Type == RuleType.TaskImmune))
                    {
                        militia.AssignmentScore++;
                    }
                }

                _context.DutyDates.Remove(dutyDate);
            }

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
