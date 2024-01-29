using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilitiaDuty.Migrations
{
    /// <inheritdoc />
    public partial class AddShifts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DutyDateId = table.Column<string>(type: "TEXT", nullable: false),
                    TaskId = table.Column<uint>(type: "INTEGER", nullable: false),
                    MilitiaId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_DutyDates_DutyDateId",
                        column: x => x.DutyDateId,
                        principalTable: "DutyDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shifts_Militias_MilitiaId",
                        column: x => x.MilitiaId,
                        principalTable: "Militias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shifts_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_DutyDateId",
                table: "Shifts",
                column: "DutyDateId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_MilitiaId",
                table: "Shifts",
                column: "MilitiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_TaskId",
                table: "Shifts",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shifts");
        }
    }
}
