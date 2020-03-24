using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStore.Data.Migrations
{
    public partial class DefaultDatesAdjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Movies",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 3, 18, 15, 34, 57, 140, DateTimeKind.Local).AddTicks(6360));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Genres",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 3, 18, 15, 34, 57, 175, DateTimeKind.Local).AddTicks(570));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Movies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 18, 15, 34, 57, 140, DateTimeKind.Local).AddTicks(6360),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Genres",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 18, 15, 34, 57, 175, DateTimeKind.Local).AddTicks(570),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
