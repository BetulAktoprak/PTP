using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PTP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProcessStages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProcessStages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProcessStages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ProcessStages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "ProcessStages");

            migrationBuilder.InsertData(
                table: "ProcessStages",
                columns: new[] { "Id", "ColorHex", "CreatedBy", "CreatedDate", "IsDeleted", "Name", "ProjectId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "#6c757d", null, new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "To Do", null, null, null },
                    { 2, "#ffc107", null, new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "In Progress", null, null, null },
                    { 3, "#198754", null, new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Done", null, null, null }
                });
        }
    }
}
