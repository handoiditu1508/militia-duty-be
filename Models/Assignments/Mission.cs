namespace MilitiaDuty.Models.Assignments
{
    public class Mission
    {
        public uint Id { get; set; }
        public required string Name { get; set; }
        public MissionStatus Status { get; set; }

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
