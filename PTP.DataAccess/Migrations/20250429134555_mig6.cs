using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
