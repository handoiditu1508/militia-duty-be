namespace MilitiaDuty.Models.Dtos
{
    public class ShiftDto
    {
        public ulong Id { get; set; }
        public required string DutyDateId { get; set; }
        public DateTime? DutyDate { get; set; }
        public uint TaskId { get; set; }
        public uint MilitiaId { get; set; }
        public string? Militia { get; set; }
    }
}
