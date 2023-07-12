using System;
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
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IOAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    AnalogInput_ScanTime = table.Column<int>(type: "INTEGER", nullable: true),
                    AnalogInput_IsScanOn = table.Column<bool>(type: "INTEGER", nullable: true),
                    AnalogInput_LowLimit = table.Column<double>(type: "REAL", nullable: true),
                    AnalogInput_HighLimit = table.Column<double>(type: "REAL", nullable: true),
                    AnalogInput_Unit = table.Column<string>(type: "TEXT", nullable: true),
                    LowLimit = table.Column<double>(type: "REAL", nullable: true),
                    HighLimit = table.Column<double>(type: "REAL", nullable: true),
                    Unit = table.Column<string>(type: "TEXT", nullable: true),
                    ScanTime = table.Column<int>(type: "INTEGER", nullable: true),
                    IsScanOn = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    AnalogInputId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alarms_Tag_AnalogInputId",
                        column: x => x.AnalogInputId,
                        principalTable: "Tag",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TagRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagRecords_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlarmRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AlarmId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmRecords_Alarms_AlarmId",
                        column: x => x.AlarmId,
                        principalTable: "Alarms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmRecords_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRecords_AlarmId",
                table: "AlarmRecords",
                column: "AlarmId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRecords_TagId",
                table: "AlarmRecords",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_AnalogInputId",
                table: "Alarms",
                column: "AnalogInputId");

            migrationBuilder.CreateIndex(
                name: "IX_TagRecords_TagId",
                table: "TagRecords",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmRecords");

            migrationBuilder.DropTable(
                name: "TagRecords");

            migrationBuilder.DropTable(
                name: "Alarms");

            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
