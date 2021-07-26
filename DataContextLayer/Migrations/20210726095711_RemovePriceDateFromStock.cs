using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.DataContextLayer.Migrations
{
    public partial class RemovePriceDateFromStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Stocks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Stocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Stocks",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
