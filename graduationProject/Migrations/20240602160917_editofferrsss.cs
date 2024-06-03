using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace graduationProject.Migrations
{
    /// <inheritdoc />
    public partial class editofferrsss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalIdInvestor",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "NationalIdUser",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "NationalcardInvestor",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "NationalcardUser",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "SignatureUser",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Offers",
                newName: "OfferValue");

            migrationBuilder.AddColumn<string>(
                name: "NationalCard",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RealName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalCard",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "OfferValue",
                table: "Offers",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "NationalIdInvestor",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalIdUser",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalcardInvestor",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalcardUser",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignatureUser",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
