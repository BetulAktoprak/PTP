using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PTP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessType",
                table: "Processes");

            migrationBuilder.AddColumn<int>(
                name: "ProcessStageId",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProcessStage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorHex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessStage", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProcessStage",
                columns: new[] { "Id", "ColorHex", "CreatedBy", "CreatedDate", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "#6c757d", null, new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "To Do", null, null },
                    { 2, "#ffc107", null, new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "In Progress", null, null },
                    { 3, "#198754", null, new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Done", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processes_ProcessStageId",
                table: "Processes",
                column: "ProcessStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_ProcessStage_ProcessStageId",
                table: "Processes",
                column: "ProcessStageId",
                principalTable: "ProcessStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_ProcessStage_ProcessStageId",
                table: "Processes");

            migrationBuilder.DropTable(
                name: "ProcessStage");

            migrationBuilder.DropIndex(
                name: "IX_Processes_ProcessStageId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "ProcessStageId",
                table: "Processes");

            migrationBuilder.AddColumn<string>(
                name: "ProcessType",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
