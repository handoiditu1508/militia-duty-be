using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilitiaDuty.Migrations
{
    /// <inheritdoc />
    public partial class AddRuleTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RuleTasks",
                columns: table => new
                {
                    RuleId = table.Column<uint>(type: "INTEGER", nullable: false),
                    TaskId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleTasks", x => new { x.TaskId, x.RuleId });
                    table.ForeignKey(
                        name: "FK_RuleTasks_Rules_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RuleTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RuleTasks_RuleId",
                table: "RuleTasks",
                column: "RuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RuleTasks");
        }
    }
}
