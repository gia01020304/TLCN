using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitoringSocialNetworkWeb.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2019, 11, 27, 10, 38, 13, 600, DateTimeKind.Local).AddTicks(1620));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: new DateTime(2019, 11, 27, 10, 38, 13, 600, DateTimeKind.Local).AddTicks(1620),
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
