using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentDetail",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Documents",
                newName: "DocumentDescriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentDescriptions",
                table: "Documents",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDetail",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
