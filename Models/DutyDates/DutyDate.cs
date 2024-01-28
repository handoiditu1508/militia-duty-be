using MilitiaDuty.Models.Militias;

namespace MilitiaDuty.Models.DutyDates
{
    public class DutyDate
    {
        public required string Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsFullDutyDate { get; set; }

        public virtual ICollection<Militia> Militias { get; set; } = new List<Militia>();
    }
}
