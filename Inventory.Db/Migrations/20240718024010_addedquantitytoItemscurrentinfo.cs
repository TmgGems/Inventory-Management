using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Management.Migrations
{
    /// <inheritdoc />
    public partial class addedquantitytoItemscurrentinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "ItemsCurrentInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "ItemsCurrentInfo");
        }
    }
}
