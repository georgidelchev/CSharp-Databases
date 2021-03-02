using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCoolCarSystem.Data.Migrations
{
    public partial class Purchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPurchase_Cars_CarId",
                table: "CarPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_CarPurchase_Customer_CustomerId",
                table: "CarPurchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarPurchase",
                table: "CarPurchase");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "CarPurchase",
                newName: "CarPurchases");

            migrationBuilder.RenameIndex(
                name: "IX_CarPurchase_CarId",
                table: "CarPurchases",
                newName: "IX_CarPurchases_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarPurchases",
                table: "CarPurchases",
                columns: new[] { "CustomerId", "CarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarPurchases_Cars_CarId",
                table: "CarPurchases",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarPurchases_Customers_CustomerId",
                table: "CarPurchases",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPurchases_Cars_CarId",
                table: "CarPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_CarPurchases_Customers_CustomerId",
                table: "CarPurchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarPurchases",
                table: "CarPurchases");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "CarPurchases",
                newName: "CarPurchase");

            migrationBuilder.RenameIndex(
                name: "IX_CarPurchases_CarId",
                table: "CarPurchase",
                newName: "IX_CarPurchase_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarPurchase",
                table: "CarPurchase",
                columns: new[] { "CustomerId", "CarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarPurchase_Cars_CarId",
                table: "CarPurchase",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarPurchase_Customer_CustomerId",
                table: "CarPurchase",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
