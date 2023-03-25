using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slayden.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentBelongsToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "post_id",
                table: "comment",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_comment_post_id",
                table: "comment",
                column: "post_id");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_post_post_id",
                table: "comment",
                column: "post_id",
                principalTable: "post",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_post_post_id",
                table: "comment");

            migrationBuilder.DropIndex(
                name: "IX_comment_post_id",
                table: "comment");

            migrationBuilder.DropColumn(
                name: "post_id",
                table: "comment");
        }
    }
}
