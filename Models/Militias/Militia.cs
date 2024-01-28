using MilitiaDuty.Models.DutyDates;

namespace MilitiaDuty.Models.Militias
{
    public class Militia
    {
        public uint Id { get; set; }
        public required string Name { get; set; }
        public ushort Score { get; set; }
        public MilitiaStatus Status { get; set; }

        public virtual ICollection<DutyDate> DutyDates { get; set; } = new List<DutyDate>();
    }
}
