using Microsoft.EntityFrameworkCore.Migrations;

namespace BookTrackingApp.Migrations
{
    public partial class BookTrackingAppMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category_CategoryId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_CategoryType_CategoryTypeId",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "CategoryTypeId",
                table: "Category",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_Category_CategoryTypeId",
                table: "Category",
                newName: "IX_Category_Type");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Book",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Book_CategoryId",
                table: "Book",
                newName: "IX_Book_Category");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category_Category",
                table: "Book",
                column: "Category",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_CategoryType_Type",
                table: "Category",
                column: "Type",
                principalTable: "CategoryType",
                principalColumn: "CategoryTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category_Category",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_CategoryType_Type",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Category",
                newName: "CategoryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Type",
                table: "Category",
                newName: "IX_Category_CategoryTypeId");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Book",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Category",
                table: "Book",
                newName: "IX_Book_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category_CategoryId",
                table: "Book",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_CategoryType_CategoryTypeId",
                table: "Category",
                column: "CategoryTypeId",
                principalTable: "CategoryType",
                principalColumn: "CategoryTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
