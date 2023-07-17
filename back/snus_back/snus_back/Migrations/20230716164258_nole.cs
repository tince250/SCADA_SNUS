using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace snus_back.Migrations
{
    /// <inheritdoc />
    public partial class nole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "HighLimit",
                table: "TagRecords",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LowLimit",
                table: "TagRecords",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighLimit",
                table: "TagRecords");

            migrationBuilder.DropColumn(
                name: "LowLimit",
                table: "TagRecords");
        }
    }
}
