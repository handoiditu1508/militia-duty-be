namespace MilitiaDuty.Models.Assignments
{
    public class Mission
    {
        public uint Id { get; set; }
        public required string Name { get; set; }
        public MissionStatus Status { get; set; }
    }
}
