using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineClinic.Migrations
{
    public partial class NewSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ETag",
                table: "Slot",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartitionKey",
                table: "Slot",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RowKey",
                table: "Slot",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Timestamp",
                table: "Slot",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ETag",
                table: "Slot");

            migrationBuilder.DropColumn(
                name: "PartitionKey",
                table: "Slot");

            migrationBuilder.DropColumn(
                name: "RowKey",
                table: "Slot");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Slot");
        }
    }
}
