namespace MilitiaDuty.Models.Requests
{
    public class DoDutyDatesRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsFullDutyDate { get; set; } = false;
    }
}
