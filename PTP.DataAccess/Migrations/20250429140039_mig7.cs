using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Personnels_PersonnelId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_PersonnelId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PersonnelId",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "PersonnelProject",
                columns: table => new
                {
                    PersonnelsId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelProject", x => new { x.PersonnelsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_PersonnelProject_Personnels_PersonnelsId",
                        column: x => x.PersonnelsId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonnelProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelProject_ProjectsId",
                table: "PersonnelProject",
                column: "ProjectsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonnelProject");

            migrationBuilder.AddColumn<int>(
                name: "PersonnelId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PersonnelId",
                table: "Projects",
                column: "PersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Personnels_PersonnelId",
                table: "Projects",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id");
        }
    }
}
