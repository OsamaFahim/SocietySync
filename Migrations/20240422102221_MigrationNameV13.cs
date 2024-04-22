using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocietySync.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNameV13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostedBySocietyName",
                table: "Announcements",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_PostedBySocietyName",
                table: "Announcements",
                column: "PostedBySocietyName");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Societies_PostedBySocietyName",
                table: "Announcements",
                column: "PostedBySocietyName",
                principalTable: "Societies",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Societies_PostedBySocietyName",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_PostedBySocietyName",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "PostedBySocietyName",
                table: "Announcements");
        }
    }
}
