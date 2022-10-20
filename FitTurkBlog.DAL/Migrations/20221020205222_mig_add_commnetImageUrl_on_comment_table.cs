using Microsoft.EntityFrameworkCore.Migrations;

namespace FitTurkBlog.DAL.Migrations
{
    public partial class mig_add_commnetImageUrl_on_comment_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentImageUrl",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentImageUrl",
                table: "Comments");
        }
    }
}
