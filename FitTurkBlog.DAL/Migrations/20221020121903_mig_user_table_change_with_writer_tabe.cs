using Microsoft.EntityFrameworkCore.Migrations;

namespace FitTurkBlog.DAL.Migrations
{
    public partial class mig_user_table_change_with_writer_tabe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages2_Writers_MessageReceiverID",
                table: "Messages2");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages2_Writers_MessageSenderID",
                table: "Messages2");

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

            migrationBuilder.AddColumn<int>(
                name: "BlogWriterId",
                table: "Blogs",
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

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogWriterId",
                table: "Blogs",
                column: "BlogWriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_BlogWriterId",
                table: "Blogs",
                column: "BlogWriterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages2_AspNetUsers_MessageReceiverID",
                table: "Messages2",
                column: "MessageReceiverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages2_AspNetUsers_MessageSenderID",
                table: "Messages2",
                column: "MessageSenderID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_BlogWriterId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages2_AspNetUsers_MessageReceiverID",
                table: "Messages2");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages2_AspNetUsers_MessageSenderID",
                table: "Messages2");

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

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BlogWriterId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "WriterID",
                table: "Messages2");

            migrationBuilder.DropColumn(
                name: "WriterID1",
                table: "Messages2");

            migrationBuilder.DropColumn(
                name: "BlogWriterId",
                table: "Blogs");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages2_Writers_MessageReceiverID",
                table: "Messages2",
                column: "MessageReceiverID",
                principalTable: "Writers",
                principalColumn: "WriterID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages2_Writers_MessageSenderID",
                table: "Messages2",
                column: "MessageSenderID",
                principalTable: "Writers",
                principalColumn: "WriterID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
