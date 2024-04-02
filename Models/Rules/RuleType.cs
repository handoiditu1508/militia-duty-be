namespace MilitiaDuty.Models.Rules
{
    public enum RuleType
    {
        /// <summary>
        /// Off on a specific date (affect onDutyScore).
        /// </summary>
        DateOff,
        /// <summary>
        /// On duty on a specific date (affect onDutyScore).
        /// </summary>
        DutyDate,
        /// <summary>
        /// Only on duty on specific days of week (not affect onDutyScore).
        /// Conflict with other duty rules.
        /// A militia should not has more than one of this rule.
        /// </summary>
        WeeklyDutyOnly,
        /// <summary>
        /// On duty without off dates for a period of time (not affect onDutyScore)
        /// </summary>
        FullDuty,
        /// <summary>
        /// Militias will alternatingly take duty each day.
        /// Conflict with other duty rules.
        /// A militia should not has more than one of this rule.
        /// </summary>
        AlternatingDuty,
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
        /// <summary>
        /// Prioritize off days for specific days of week (affect AssignmentScore)
        /// </summary>
        PreferOffDays,
        /// <summary>
        /// Ensure to be off on specific days in week
        /// </summary>
        WeeklyOffDays,
    }
}
