using MilitiaDuty.Models.Militias;

namespace MilitiaDuty.Models.Dtos
{
    public class MilitiaDto
    {
        public uint Id { get; set; }
        public required string Name { get; set; }
        public float DutyDateScore { get; set; }
        public int AssignmentScore { get; set; }
        public MilitiaStatus Status { get; set; }
    }
}
