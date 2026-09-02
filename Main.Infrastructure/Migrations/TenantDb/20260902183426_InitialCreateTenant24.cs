using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations.TenantDb
{
    /// <inheritdoc />
    public partial class InitialCreateTenant24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FiePath",
                table: "ProductImageFiles");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ProductImageFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductOwner",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryID",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 4,
                column: "EnumPublicPage",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 5,
                column: "EnumPublicPage",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 6,
                column: "EnumPublicPage",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 7,
                column: "EnumPublicPage",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 9,
                column: "EnumPublicPage",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 13,
                column: "EnumPublicPage",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 14,
                column: "EnumPublicPage",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 15,
                column: "EnumPublicPage",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 16,
                column: "EnumPublicPage",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 18,
                column: "EnumPublicPage",
                value: 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ProductImageFiles");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ProductOwner",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SubCategoryID",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "FiePath",
                table: "ProductImageFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 4,
                column: "EnumPublicPage",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 5,
                column: "EnumPublicPage",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 6,
                column: "EnumPublicPage",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 7,
                column: "EnumPublicPage",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 9,
                column: "EnumPublicPage",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 13,
                column: "EnumPublicPage",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 14,
                column: "EnumPublicPage",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 15,
                column: "EnumPublicPage",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 16,
                column: "EnumPublicPage",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 18,
                column: "EnumPublicPage",
                value: 9);
        }
    }
}
