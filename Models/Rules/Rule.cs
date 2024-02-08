using MilitiaDuty.Models.Militias;

namespace MilitiaDuty.Models.Rules
{
    public class Rule
    {
        public uint Id { get; set; }
        public required DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public RuleType Type { get; set; }
        public string? Description { get; set; }
        public IEnumerable<DayOfWeek>? Weekdays { get; set; }
        public ushort? OnDutyRate { get; set; }

        public virtual ICollection<Militia> Militias { get; set; } = new List<Militia>();
        public virtual ICollection<Assignments.Task> Tasks { get; set; } = new List<Assignments.Task>();
    }
}
