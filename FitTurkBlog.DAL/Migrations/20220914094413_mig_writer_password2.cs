using Microsoft.EntityFrameworkCore.Migrations;

namespace FitTurkBlog.DAL.Migrations
{
    public partial class mig_writer_password2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WriterPassword2",
                table: "Writers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WriterPassword2",
                table: "Writers");
        }
    }
}
