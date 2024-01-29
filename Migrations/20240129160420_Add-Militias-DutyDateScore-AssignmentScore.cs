using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilitiaDuty.Migrations
{
    /// <inheritdoc />
    public partial class AddMilitiasDutyDateScoreAssignmentScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Militias",
                newName: "DutyDateScore");

            migrationBuilder.AddColumn<ushort>(
                name: "AssignmentScore",
                table: "Militias",
                type: "INTEGER",
                nullable: false,
                defaultValue: (ushort)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentScore",
                table: "Militias");

            migrationBuilder.RenameColumn(
                name: "DutyDateScore",
                table: "Militias",
                newName: "Score");
        }
    }
}
