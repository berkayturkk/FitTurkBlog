using Microsoft.EntityFrameworkCore.Migrations;

namespace FitTurkBlog.DAL.Migrations
{
    public partial class mig_identity_add_appuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameSurname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "İmageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameSurname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "İmageUrl",
                table: "AspNetUsers");
        }
    }
}
