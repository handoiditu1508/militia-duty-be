namespace MilitiaDuty.Models.Dtos
{
    public class TaskDto
    {
        public uint Id { get; set; }
        public uint MissionId { get; set; }
        public uint StartMinute { get; set; }
        public uint EndMinute { get; set; }
        public uint MilitiaNumber { get; set; }
    }
}
