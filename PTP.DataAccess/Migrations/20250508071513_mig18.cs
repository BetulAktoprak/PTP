using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectPersonnels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectPersonnels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProjectPersonnels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectPersonnels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ProjectPersonnels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProjectPersonnels",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectPersonnels");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectPersonnels");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectPersonnels");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectPersonnels");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProjectPersonnels");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProjectPersonnels");
        }
    }
}
