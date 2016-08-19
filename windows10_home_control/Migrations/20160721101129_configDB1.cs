using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace windows10_home_control.Migrations
{
    public partial class configDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    DeviceAlarmState = table.Column<int>(nullable: false),
                    DeviceEnable = table.Column<bool>(nullable: false),
                    DeviceName = table.Column<string>(nullable: true),
                    DevicePin = table.Column<int>(nullable: false),
                    DeviceState = table.Column<int>(nullable: false),
                    DeviceType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
