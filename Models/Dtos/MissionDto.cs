using MilitiaDuty.Models.Assignments;

namespace MilitiaDuty.Models.Dtos
{
    public class MissionDto
    {
        public uint Id { get; set; }
        public required string Name { get; set; }
        public MissionStatus Status { get; set; }

        public IEnumerable<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
