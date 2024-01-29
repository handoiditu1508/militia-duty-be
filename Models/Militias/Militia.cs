using MilitiaDuty.Models.Assignments;
using MilitiaDuty.Models.DutyDates;
using MilitiaDuty.Models.Rules;

namespace MilitiaDuty.Models.Militias
{
    public class Militia
    {
        public uint Id { get; set; }
        public required string Name { get; set; }
        public ushort Score { get; set; }
        public MilitiaStatus Status { get; set; }

        public virtual ICollection<DutyDate> DutyDates { get; set; } = new List<DutyDate>();
        public virtual ICollection<Rule> Rules { get; set; } = new List<Rule>();
        public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
