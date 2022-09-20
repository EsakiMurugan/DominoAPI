using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domino.Migrations
{
    public partial class mig101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_customers_CustomerID",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_pizza_PizzaID",
                table: "cart");

            migrationBuilder.AlterColumn<float>(
                name: "UnitPrice",
                table: "cart",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "cart",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PizzaID",
                table: "cart",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "cart",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CartTypeID",
                table: "cart",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_customers_CustomerID",
                table: "cart",
                column: "CustomerID",
                principalTable: "customers",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_pizza_PizzaID",
                table: "cart",
                column: "PizzaID",
                principalTable: "pizza",
                principalColumn: "PizzaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_customers_CustomerID",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_pizza_PizzaID",
                table: "cart");

            migrationBuilder.AlterColumn<float>(
                name: "UnitPrice",
                table: "cart",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "cart",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PizzaID",
                table: "cart",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "cart",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CartTypeID",
                table: "cart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_customers_CustomerID",
                table: "cart",
                column: "CustomerID",
                principalTable: "customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_pizza_PizzaID",
                table: "cart",
                column: "PizzaID",
                principalTable: "pizza",
                principalColumn: "PizzaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
