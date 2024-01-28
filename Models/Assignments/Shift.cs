namespace MilitiaDuty.Models.Assignments
{
    public class Shift
    {
        public ulong Id { get; set; }
        public required string DutyDateId { get; set; }
        public uint TaskId { get; set; }
        public uint MilitiaId { get; set; }
    }
}
