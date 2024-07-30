using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Management.Migrations
{
    /// <inheritdoc />
    public partial class addedquantitytoItemsHistroyinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ItemsHistoryInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ItemsHistoryInfo");
        }
    }
}
