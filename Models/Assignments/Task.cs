using MilitiaDuty.Models.Rules;

namespace MilitiaDuty.Models.Assignments
{
    public class Task
    {
        public uint Id { get; set; }
        public uint MissionId { get; set; }
        public Mission? Mission { get; set; }
        public uint StartMinute { get; set; }
        public uint EndMinute { get; set; }
        public uint MilitiaNumber { get; set; }

        public virtual ICollection<Rule> Rules { get; set; } = new List<Rule>();
        public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
