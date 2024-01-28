namespace MilitiaDuty.Models.Rules
{
    public class Rule
    {
        public uint Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RuleType Type { get; set; }
    }
}
