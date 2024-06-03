using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace graduationProject.Migrations
{
    /// <inheritdoc />
    public partial class AddConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_User1Id",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_User2Id",
                table: "Connections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connections",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "Connections");

            migrationBuilder.RenameColumn(
                name: "User2Id",
                table: "Connections",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "User1Id",
                table: "Connections",
                newName: "ReceiverId");

            migrationBuilder.RenameColumn(
                name: "IsAgree",
                table: "Connections",
                newName: "IsConnected");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_User2Id",
                table: "Connections",
                newName: "IX_Connections_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_User1Id",
                table: "Connections",
                newName: "IX_Connections_ReceiverId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Connections",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connections",
                table: "Connections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_ReceiverId",
                table: "Connections",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_SenderId",
                table: "Connections",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_ReceiverId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_SenderId",
                table: "Connections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connections",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Connections");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Connections",
                newName: "User2Id");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Connections",
                newName: "User1Id");

            migrationBuilder.RenameColumn(
                name: "IsConnected",
                table: "Connections",
                newName: "IsAgree");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_SenderId",
                table: "Connections",
                newName: "IX_Connections_User2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_ReceiverId",
                table: "Connections",
                newName: "IX_Connections_User1Id");

            migrationBuilder.AddColumn<string>(
                name: "ConnectionId",
                table: "Connections",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connections",
                table: "Connections",
                column: "ConnectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_User1Id",
                table: "Connections",
                column: "User1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_User2Id",
                table: "Connections",
                column: "User2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
