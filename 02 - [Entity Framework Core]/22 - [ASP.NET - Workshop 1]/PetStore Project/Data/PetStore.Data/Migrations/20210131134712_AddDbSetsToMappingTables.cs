using Microsoft.EntityFrameworkCore.Migrations;

namespace PetStore.Data.Migrations
{
    public partial class AddDbSetsToMappingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodOrder_Foods_FoodId",
                table: "FoodOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodOrder_Orders_OrderId",
                table: "FoodOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ToyOrder_Orders_OrderId",
                table: "ToyOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ToyOrder_Toys_ToyId",
                table: "ToyOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToyOrder",
                table: "ToyOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodOrder",
                table: "FoodOrder");

            migrationBuilder.RenameTable(
                name: "ToyOrder",
                newName: "ToyOrders");

            migrationBuilder.RenameTable(
                name: "FoodOrder",
                newName: "FoodOrders");

            migrationBuilder.RenameIndex(
                name: "IX_ToyOrder_OrderId",
                table: "ToyOrders",
                newName: "IX_ToyOrders_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodOrder_OrderId",
                table: "FoodOrders",
                newName: "IX_FoodOrders_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToyOrders",
                table: "ToyOrders",
                columns: new[] { "ToyId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodOrders",
                table: "FoodOrders",
                columns: new[] { "FoodId", "OrderId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FoodOrders_Foods_FoodId",
                table: "FoodOrders",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodOrders_Orders_OrderId",
                table: "FoodOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToyOrders_Orders_OrderId",
                table: "ToyOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToyOrders_Toys_ToyId",
                table: "ToyOrders",
                column: "ToyId",
                principalTable: "Toys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodOrders_Foods_FoodId",
                table: "FoodOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodOrders_Orders_OrderId",
                table: "FoodOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ToyOrders_Orders_OrderId",
                table: "ToyOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ToyOrders_Toys_ToyId",
                table: "ToyOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToyOrders",
                table: "ToyOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodOrders",
                table: "FoodOrders");

            migrationBuilder.RenameTable(
                name: "ToyOrders",
                newName: "ToyOrder");

            migrationBuilder.RenameTable(
                name: "FoodOrders",
                newName: "FoodOrder");

            migrationBuilder.RenameIndex(
                name: "IX_ToyOrders_OrderId",
                table: "ToyOrder",
                newName: "IX_ToyOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodOrders_OrderId",
                table: "FoodOrder",
                newName: "IX_FoodOrder_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToyOrder",
                table: "ToyOrder",
                columns: new[] { "ToyId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodOrder",
                table: "FoodOrder",
                columns: new[] { "FoodId", "OrderId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FoodOrder_Foods_FoodId",
                table: "FoodOrder",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodOrder_Orders_OrderId",
                table: "FoodOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToyOrder_Orders_OrderId",
                table: "ToyOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToyOrder_Toys_ToyId",
                table: "ToyOrder",
                column: "ToyId",
                principalTable: "Toys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
