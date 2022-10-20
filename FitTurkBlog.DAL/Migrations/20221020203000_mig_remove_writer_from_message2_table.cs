using Microsoft.EntityFrameworkCore.Migrations;

namespace FitTurkBlog.DAL.Migrations
{
    public partial class mig_remove_writer_from_message2_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages2_Writers_WriterID",
                table: "Messages2");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages2_Writers_WriterID1",
                table: "Messages2");

            migrationBuilder.DropIndex(
                name: "IX_Messages2_WriterID",
                table: "Messages2");

            migrationBuilder.DropIndex(
                name: "IX_Messages2_WriterID1",
                table: "Messages2");

            migrationBuilder.DropColumn(
                name: "WriterID",
                table: "Messages2");

            migrationBuilder.DropColumn(
                name: "WriterID1",
                table: "Messages2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WriterID",
                table: "Messages2",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WriterID1",
                table: "Messages2",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages2_WriterID",
                table: "Messages2",
                column: "WriterID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages2_WriterID1",
                table: "Messages2",
                column: "WriterID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages2_Writers_WriterID",
                table: "Messages2",
                column: "WriterID",
                principalTable: "Writers",
                principalColumn: "WriterID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages2_Writers_WriterID1",
                table: "Messages2",
                column: "WriterID1",
                principalTable: "Writers",
                principalColumn: "WriterID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
