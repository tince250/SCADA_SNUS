using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace snus_back.Migrations
{
    /// <inheritdoc />
    public partial class updatedmode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlarmRecords_Tag_TagId",
                table: "AlarmRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Alarms_Tag_AnalogInputId",
                table: "Alarms");

            migrationBuilder.DropForeignKey(
                name: "FK_TagRecords_Tag_TagId",
                table: "TagRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlarmRecords_Tags_TagId",
                table: "AlarmRecords",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alarms_Tags_AnalogInputId",
                table: "Alarms",
                column: "AnalogInputId",
                principalTable: "Tags",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagRecords_Tags_TagId",
                table: "TagRecords",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlarmRecords_Tags_TagId",
                table: "AlarmRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Alarms_Tags_AnalogInputId",
                table: "Alarms");

            migrationBuilder.DropForeignKey(
                name: "FK_TagRecords_Tags_TagId",
                table: "TagRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlarmRecords_Tag_TagId",
                table: "AlarmRecords",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alarms_Tag_AnalogInputId",
                table: "Alarms",
                column: "AnalogInputId",
                principalTable: "Tag",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagRecords_Tag_TagId",
                table: "TagRecords",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
