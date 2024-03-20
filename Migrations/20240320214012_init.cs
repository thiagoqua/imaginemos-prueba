using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VentaId",
                table: "SellDetails",
                newName: "SellId");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_UserId",
                table: "Sells",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SellDetails_ProductId",
                table: "SellDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SellDetails_SellId",
                table: "SellDetails",
                column: "SellId");

            migrationBuilder.AddForeignKey(
                name: "FK_SellDetails_Products_ProductId",
                table: "SellDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellDetails_Sells_SellId",
                table: "SellDetails",
                column: "SellId",
                principalTable: "Sells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sells_Users_UserId",
                table: "Sells",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellDetails_Products_ProductId",
                table: "SellDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SellDetails_Sells_SellId",
                table: "SellDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Sells_Users_UserId",
                table: "Sells");

            migrationBuilder.DropIndex(
                name: "IX_Sells_UserId",
                table: "Sells");

            migrationBuilder.DropIndex(
                name: "IX_SellDetails_ProductId",
                table: "SellDetails");

            migrationBuilder.DropIndex(
                name: "IX_SellDetails_SellId",
                table: "SellDetails");

            migrationBuilder.RenameColumn(
                name: "SellId",
                table: "SellDetails",
                newName: "VentaId");
        }
    }
}
