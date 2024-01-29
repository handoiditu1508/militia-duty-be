using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilitiaDuty.Migrations
{
    /// <inheritdoc />
    public partial class AddMilitiasDutyDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DutyDates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsFullDutyDate = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Militias",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Score = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Militias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MilitiaDutyDates",
                columns: table => new
                {
                    MilitiaId = table.Column<uint>(type: "INTEGER", nullable: false),
                    DutyDateId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitiaDutyDates", x => new { x.MilitiaId, x.DutyDateId });
                    table.ForeignKey(
                        name: "FK_MilitiaDutyDates_DutyDates_DutyDateId",
                        column: x => x.DutyDateId,
                        principalTable: "DutyDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MilitiaDutyDates_Militias_MilitiaId",
                        column: x => x.MilitiaId,
                        principalTable: "Militias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MilitiaDutyDates_DutyDateId",
                table: "MilitiaDutyDates",
                column: "DutyDateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MilitiaDutyDates");

            migrationBuilder.DropTable(
                name: "DutyDates");

            migrationBuilder.DropTable(
                name: "Militias");
        }
    }
}
