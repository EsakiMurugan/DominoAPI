using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domino.Migrations
{
    public partial class Receipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_customers_CustomerID",
                table: "payments");

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "receipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiptDate",
                table: "receipts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "pizza",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "pizza",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "TotalAmount",
                table: "payments",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CardNumber",
                table: "payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "MobileNumber",
                table: "customers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_receipts_CartID",
                table: "receipts",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_customers_CustomerID",
                table: "payments",
                column: "CustomerID",
                principalTable: "customers",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_receipts_cart_CartID",
                table: "receipts",
                column: "CartID",
                principalTable: "cart",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_customers_CustomerID",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_receipts_cart_CartID",
                table: "receipts");

            migrationBuilder.DropIndex(
                name: "IX_receipts_CartID",
                table: "receipts");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "receipts");

            migrationBuilder.DropColumn(
                name: "ReceiptDate",
                table: "receipts");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "pizza",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "pizza",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "TotalAmount",
                table: "payments",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CardNumber",
                table: "payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MobileNumber",
                table: "customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_customers_CustomerID",
                table: "payments",
                column: "CustomerID",
                principalTable: "customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
