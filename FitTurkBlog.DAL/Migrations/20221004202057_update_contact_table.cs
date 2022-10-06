using Microsoft.EntityFrameworkCore.Migrations;

namespace FitTurkBlog.DAL.Migrations
{
    public partial class update_contact_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactAdress",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "Contacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactAdress",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
