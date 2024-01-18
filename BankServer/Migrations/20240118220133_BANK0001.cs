using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankServer.Migrations
{
    public partial class BANK0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PersonId",
                table: "Accounts",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_PersonId",
                table: "Accounts",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_PersonId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PersonId",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
