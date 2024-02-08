using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilitiaDuty.Migrations
{
    /// <inheritdoc />
    public partial class AddRuleDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rules",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rules");
        }
    }
}
