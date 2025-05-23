using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Personnels_PersonnelId",
                table: "Processes");

            migrationBuilder.AlterColumn<int>(
                name: "PersonnelId",
                table: "Processes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Personnels_PersonnelId",
                table: "Processes",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Personnels_PersonnelId",
                table: "Processes");

            migrationBuilder.AlterColumn<int>(
                name: "PersonnelId",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Personnels_PersonnelId",
                table: "Processes",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
