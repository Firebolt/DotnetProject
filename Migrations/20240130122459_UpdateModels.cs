using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UID",
                table: "Queries");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Queries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Queries");

            migrationBuilder.AddColumn<int>(
                name: "UID",
                table: "Queries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
