﻿namespace MilitiaDuty.Models.Dtos
{
    public class DutyDateDto
    {
        public required string Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsFullDutyDate { get; set; }

        public IEnumerable<MilitiaDto> Militias { get; set; } = new List<MilitiaDto>();
        public IEnumerable<ShiftDto> Shifts { get; set; } = new List<ShiftDto>();
    }
}
