namespace MilitiaDuty.Models.Dtos
{
    public class DutyDateDto
    {
        public required string Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsFullDutyDate { get; set; }

        public required IEnumerable<MilitiaDto> Militias { get; set; }
        public required IEnumerable<ShiftDto> Shifts { get; set; }
    }
}
