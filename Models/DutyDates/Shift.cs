using MilitiaDuty.Models.Militias;

namespace MilitiaDuty.Models.DutyDates
{
    public class Shift
    {
        public ulong Id { get; set; }
        public required string DutyDateId { get; set; }
        public DutyDate? DutyDate { get; set; }
        public uint TaskId { get; set; }
        public Assignments.Task? Task { get; set; }
        public uint MilitiaId { get; set; }
        public Militia? Militia { get; set; }
    }
}
