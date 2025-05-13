using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_ProcessStage_ProcessStageId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessStage_Projects_ProjectId",
                table: "ProcessStage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessStage",
                table: "ProcessStage");

            migrationBuilder.RenameTable(
                name: "ProcessStage",
                newName: "ProcessStages");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessStage_ProjectId",
                table: "ProcessStages",
                newName: "IX_ProcessStages_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessStages",
                table: "ProcessStages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_ProcessStages_ProcessStageId",
                table: "Processes",
                column: "ProcessStageId",
                principalTable: "ProcessStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessStages_Projects_ProjectId",
                table: "ProcessStages",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_ProcessStages_ProcessStageId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessStages_Projects_ProjectId",
                table: "ProcessStages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessStages",
                table: "ProcessStages");

            migrationBuilder.RenameTable(
                name: "ProcessStages",
                newName: "ProcessStage");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessStages_ProjectId",
                table: "ProcessStage",
                newName: "IX_ProcessStage_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessStage",
                table: "ProcessStage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_ProcessStage_ProcessStageId",
                table: "Processes",
                column: "ProcessStageId",
                principalTable: "ProcessStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessStage_Projects_ProjectId",
                table: "ProcessStage",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
