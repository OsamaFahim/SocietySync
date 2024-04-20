using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocietySync.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNameV6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocietyMemberships",
                columns: table => new
                {
                    Member_RollNum = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Society_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRollNum = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SocietyName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ID", x => new { x.Member_RollNum, x.Society_Name });
                    table.ForeignKey(
                        name: "FK_SocietyMemberships_Societies_SocietyName",
                        column: x => x.SocietyName,
                        principalTable: "Societies",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_SocietyMemberships_Users_UserRollNum",
                        column: x => x.UserRollNum,
                        principalTable: "Users",
                        principalColumn: "RollNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocietyMemberships_SocietyName",
                table: "SocietyMemberships",
                column: "SocietyName");

            migrationBuilder.CreateIndex(
                name: "IX_SocietyMemberships_UserRollNum",
                table: "SocietyMemberships",
                column: "UserRollNum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocietyMemberships");
        }
    }
}
