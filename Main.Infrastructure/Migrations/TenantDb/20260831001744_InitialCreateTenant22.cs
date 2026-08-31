using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations.TenantDb
{
    /// <inheritdoc />
    public partial class InitialCreateTenant22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SearchTag",
                table: "Products",
                newName: "NameTag");

            migrationBuilder.AddColumn<string>(
                name: "ProductOwner",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOwner",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "NameTag",
                table: "Products",
                newName: "SearchTag");
        }
    }
}
