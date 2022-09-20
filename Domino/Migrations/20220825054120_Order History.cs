using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domino.Migrations
{
    public partial class OrderHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_receipts_cart_CartID",
                table: "receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_receipts_payments_PaymentID",
                table: "receipts");

            migrationBuilder.DropIndex(
                name: "IX_receipts_CartID",
                table: "receipts");

            migrationBuilder.DropIndex(
                name: "IX_receipts_PaymentID",
                table: "receipts");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "receipts");

            migrationBuilder.DropColumn(
                name: "PaymentID",
                table: "receipts");

            migrationBuilder.AddColumn<int>(
                name: "PizzaID",
                table: "receipts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "receipts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "UnitPrice",
                table: "receipts",
                type: "real",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_receipts_PizzaID",
                table: "receipts",
                column: "PizzaID");

            migrationBuilder.AddForeignKey(
                name: "FK_receipts_pizza_PizzaID",
                table: "receipts",
                column: "PizzaID",
                principalTable: "pizza",
                principalColumn: "PizzaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_receipts_pizza_PizzaID",
                table: "receipts");

            migrationBuilder.DropIndex(
                name: "IX_receipts_PizzaID",
                table: "receipts");

            migrationBuilder.DropColumn(
                name: "PizzaID",
                table: "receipts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "receipts");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "receipts");

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "receipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentID",
                table: "receipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_receipts_CartID",
                table: "receipts",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_receipts_PaymentID",
                table: "receipts",
                column: "PaymentID");

            migrationBuilder.AddForeignKey(
                name: "FK_receipts_cart_CartID",
                table: "receipts",
                column: "CartID",
                principalTable: "cart",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_receipts_payments_PaymentID",
                table: "receipts",
                column: "PaymentID",
                principalTable: "payments",
                principalColumn: "PaymentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
