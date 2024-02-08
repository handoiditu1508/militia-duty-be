using MilitiaDuty.Models.Rules;

namespace MilitiaDuty.Models.Assignments
{
    public class Task
    {
        public uint Id { get; set; }
        public uint MissionId { get; set; }
        public Mission? Mission { get; set; }
        public ushort StartMinute { get; set; }
        public ushort EndMinute { get; set; }
        public ushort MilitiaNumber { get; set; }

        public virtual ICollection<Rule> Rules { get; set; } = new List<Rule>();
        public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
