using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaWebAPI.Migrations
{
    public partial class RenamePropertyInPizza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ingredients",
                table: "Pizzas",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Pizzas",
                newName: "Ingredients");
        }
    }
}
