namespace MilitiaDuty.Models.Options
{
    public class MilitiaOptions
    {
        public const string Militia = "Militia";

        public ushort IdealMilitiaPerDate { get; set; }
        public float OnDutyRate { get; set; }
    }
}
