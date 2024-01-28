namespace MilitiaDuty.Models.Assignments
{
    public class Task
    {
        public uint Id { get; set; }
        public uint MissionId { get; set; }
        public ushort StartMinute { get; set; }
        public ushort EndMinute { get; set; }
    }
}
