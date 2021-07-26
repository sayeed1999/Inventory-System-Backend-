using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.DataContextLayer.Migrations
{
    public partial class RenameDatePurchasedToDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DatePurchased",
                table: "Stocks",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Stocks",
                newName: "DatePurchased");
        }
    }
}
