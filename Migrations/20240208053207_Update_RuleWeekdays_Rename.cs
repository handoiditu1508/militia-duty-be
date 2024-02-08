using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilitiaDuty.Migrations
{
    /// <inheritdoc />
    public partial class Update_RuleWeekdays_Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeekDays",
                table: "Rules",
                newName: "Weekdays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weekdays",
                table: "Rules",
                newName: "WeekDays");
        }
    }
}
