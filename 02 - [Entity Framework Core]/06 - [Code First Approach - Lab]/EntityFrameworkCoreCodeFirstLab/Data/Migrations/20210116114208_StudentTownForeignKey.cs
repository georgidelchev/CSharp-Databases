using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCoreCodeFirstLab.Data.Migrations
{
    public partial class StudentTownForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TownId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_TownId",
                table: "Students",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Towns_TownId",
                table: "Students",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Towns_TownId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropIndex(
                name: "IX_Students_TownId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TownId",
                table: "Students");
        }
    }
}
