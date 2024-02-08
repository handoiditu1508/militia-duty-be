namespace MilitiaDuty.Models.Rules
{
    public enum RuleType
    {
        /// <summary>
        /// Off on a specific date (affect onDutyScore)
        /// </summary>
        DateOff,
        /// <summary>
        /// On duty on a specific date (affect onDutyScore)
        /// </summary>
        DutyDate,
        /// <summary>
        /// Only on duty on specific days of week (not affect onDutyScore)
        /// </summary>
        WeeklyDutyOnly,
        /// <summary>
        /// On duty without off dates for a period of time (not affect onDutyScore)
        /// </summary>
        FullDuty,
        /// <summary>
        /// Two or more militias can not all off on a same date
        /// </summary>
        NoAllOff,
        /// <summary>
        /// Override default OnDutyRate (not affect onDutyScore)
        /// </summary>
        OnDutyRate,
        /// <summary>
        /// Only do specific tasks (affect AssignmentScore)
        /// </summary>
        IncludeTasks,
        /// <summary>
        /// Not do specific tasks (affect AssignmentScore)
        /// </summary>
        ExcludeTasks,
        /// <summary>
        /// Never take any task (not affect AssignmentScore)
        /// </summary>
        TaskImmune,
    }
}
