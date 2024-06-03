using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace graduationProject.Migrations
{
    /// <inheritdoc />
    public partial class EdittcommentPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Comments");
        }
    }
}
