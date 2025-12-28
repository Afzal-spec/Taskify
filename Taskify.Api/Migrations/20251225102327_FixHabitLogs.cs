using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskify.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixHabitLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitLogs_Users_UserId",
                table: "HabitLogs");

            migrationBuilder.DropIndex(
                name: "IX_HabitLogs_UserId",
                table: "HabitLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HabitLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "HabitLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HabitLogs_UserId",
                table: "HabitLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HabitLogs_Users_UserId",
                table: "HabitLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
