using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Persistence.Migrations
{
    public partial class BasketItemsTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "BasketItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_UserID",
                table: "BasketItems",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_AspNetUsers_UserID",
                table: "BasketItems",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_AspNetUsers_UserID",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_UserID",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "BasketItems");
        }
    }
}
