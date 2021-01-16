using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCoreCodeFirstLab.Data.Migrations
{
    public partial class StudentHasScholarshipColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasScholarship",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasScholarship",
                table: "Students");
        }
    }
}
