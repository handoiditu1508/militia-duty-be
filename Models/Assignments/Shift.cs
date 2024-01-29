using MilitiaDuty.Models.DutyDates;
using MilitiaDuty.Models.Militias;

namespace MilitiaDuty.Models.Assignments
{
    public class Shift
    {
        public ulong Id { get; set; }
        public required string DutyDateId { get; set; }
        public DutyDate? DutyDate { get; set; }
        public uint TaskId { get; set; }
        public Task? Task { get; set; }
        public uint MilitiaId { get; set; }
        public Militia? Militia { get; set; }
    }
}
