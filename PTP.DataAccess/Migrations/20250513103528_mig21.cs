using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ProcessStage",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ProcessStage",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProcessStage",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProjectId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProcessStage",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProjectId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessStage_ProjectId",
                table: "ProcessStage",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessStage_Projects_ProjectId",
                table: "ProcessStage",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessStage_Projects_ProjectId",
                table: "ProcessStage");

            migrationBuilder.DropIndex(
                name: "IX_ProcessStage_ProjectId",
                table: "ProcessStage");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProcessStage");
        }
    }
}
