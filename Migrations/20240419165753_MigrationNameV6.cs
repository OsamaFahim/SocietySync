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
                    Society_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Society = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocietyName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocietyMemberships", x => x.Member_RollNum);
                    table.ForeignKey(
                        name: "FK_SocietyMemberships_Societies_SocietyName",
                        column: x => x.SocietyName,
                        principalTable: "Societies",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_SocietyMemberships_Users_Member_RollNum",
                        column: x => x.Member_RollNum,
                        principalTable: "Users",
                        principalColumn: "RollNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocietyMemberships_SocietyName",
                table: "SocietyMemberships",
                column: "SocietyName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocietyMemberships");
        }
    }
}
