using MilitiaDuty.Models.Rules;

namespace MilitiaDuty.Models.Dtos
{
    public class RuleDto
    {
        public uint Id { get; set; }
        public required DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public RuleType Type { get; set; }
        public string? Description { get; set; }
        public IEnumerable<DayOfWeek>? Weekdays { get; set; }
        public float? NumberValue { get; set; }

        public IEnumerable<MilitiaDto> Militias { get; set; } = new List<MilitiaDto>();
        public IEnumerable<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
