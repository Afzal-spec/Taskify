using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskify.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddHabitLogsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HabitLogs_HabitId",
                table: "HabitLogs");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "HabitLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HabitLogs_HabitId_Date",
                table: "HabitLogs",
                columns: new[] { "HabitId", "Date" },
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitLogs_Users_UserId",
                table: "HabitLogs");

            migrationBuilder.DropIndex(
                name: "IX_HabitLogs_HabitId_Date",
                table: "HabitLogs");

            migrationBuilder.DropIndex(
                name: "IX_HabitLogs_UserId",
                table: "HabitLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HabitLogs");

            migrationBuilder.CreateIndex(
                name: "IX_HabitLogs_HabitId",
                table: "HabitLogs",
                column: "HabitId");
        }
    }
}
