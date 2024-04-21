using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocietySync.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNameV11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_PostedByUserId",
                table: "Announcements");

            migrationBuilder.AlterColumn<string>(
                name: "PostedByUserId",
                table: "Announcements",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_PostedByUserId",
                table: "Announcements",
                column: "PostedByUserId",
                principalTable: "Users",
                principalColumn: "RollNum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_PostedByUserId",
                table: "Announcements");

            migrationBuilder.AlterColumn<string>(
                name: "PostedByUserId",
                table: "Announcements",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_PostedByUserId",
                table: "Announcements",
                column: "PostedByUserId",
                principalTable: "Users",
                principalColumn: "RollNum",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
