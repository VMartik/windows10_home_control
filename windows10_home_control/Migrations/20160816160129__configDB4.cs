using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace windows10_home_control.Migrations
{
    public partial class _configDB4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rFidCard");

            migrationBuilder.CreateTable(
                name: "rFidCards",
                columns: table => new
                {
                    rFidCardId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    rFidType = table.Column<bool>(nullable: false),
                    rFidUID = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rFidCards", x => x.rFidCardId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rFidCards");

            migrationBuilder.CreateTable(
                name: "rFidCard",
                columns: table => new
                {
                    rFidCardsId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    rFidType = table.Column<bool>(nullable: false),
                    rFidUID = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rFidCard", x => x.rFidCardsId);
                });
        }
    }
}
