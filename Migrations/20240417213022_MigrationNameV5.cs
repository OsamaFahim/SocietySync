using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocietySync.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNameV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Societies",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Goals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PresidentRollNum = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Societies", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Societies_Users_PresidentRollNum",
                        column: x => x.PresidentRollNum,
                        principalTable: "Users",
                        principalColumn: "RollNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Societies_PresidentRollNum",
                table: "Societies",
                column: "PresidentRollNum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Societies");
        }
    }
}
