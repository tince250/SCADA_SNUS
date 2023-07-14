using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace snus_back.Migrations
{
    /// <inheritdoc />
    public partial class updatedmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnalogInputId",
                table: "AlarmRecords",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRecords_AnalogInputId",
                table: "AlarmRecords",
                column: "AnalogInputId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlarmRecords_Tag_AnalogInputId",
                table: "AlarmRecords",
                column: "AnalogInputId",
                principalTable: "Tag",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlarmRecords_Tag_AnalogInputId",
                table: "AlarmRecords");

            migrationBuilder.DropIndex(
                name: "IX_AlarmRecords_AnalogInputId",
                table: "AlarmRecords");

            migrationBuilder.DropColumn(
                name: "AnalogInputId",
                table: "AlarmRecords");
        }
    }
}
