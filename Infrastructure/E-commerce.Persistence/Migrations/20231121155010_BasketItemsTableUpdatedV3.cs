using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Persistence.Migrations
{
    public partial class BasketItemsTableUpdatedV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_AspNetUsers_UserID",
                table: "BasketItems");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "BasketItems",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_UserID",
                table: "BasketItems",
                newName: "IX_BasketItems_AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_AspNetUsers_AppUserID",
                table: "BasketItems",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_AspNetUsers_AppUserID",
                table: "BasketItems");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "BasketItems",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_AppUserID",
                table: "BasketItems",
                newName: "IX_BasketItems_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_AspNetUsers_UserID",
                table: "BasketItems",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
