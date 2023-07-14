using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace snus_back.Migrations
{
    /// <inheritdoc />
    public partial class updatediomodel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "AlarmRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRecords_TagId",
                table: "AlarmRecords",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlarmRecords_Tag_TagId",
                table: "AlarmRecords",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlarmRecords_Tag_TagId",
                table: "AlarmRecords");

            migrationBuilder.DropIndex(
                name: "IX_AlarmRecords_TagId",
                table: "AlarmRecords");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "AlarmRecords");
        }
    }
}
