using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Main.Infrastructure.Migrations.TenantDb
{
    /// <inheritdoc />
    public partial class InitialCreateTenant1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminPosts",
                columns: table => new
                {
                    AdminPostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostType = table.Column<int>(type: "int", nullable: false),
                    PosterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterContactNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortNote = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    SearchTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminPosts", x => x.AdminPostID);
                });

            migrationBuilder.CreateTable(
                name: "AllowedValues",
                columns: table => new
                {
                    ValueID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Variable = table.Column<int>(type: "int", nullable: false),
                    ParentValueId = table.Column<long>(type: "bigint", nullable: false),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedValues", x => x.ValueID);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnumPublicPage = table.Column<int>(type: "int", nullable: false),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostType = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    SubCategoryID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    SaleCommission = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    SearchTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "AdminImageFiles",
                columns: table => new
                {
                    AdminImageFileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageFileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPostID = table.Column<int>(type: "int", nullable: false),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminImageFiles", x => x.AdminImageFileID);
                    table.ForeignKey(
                        name: "FK_AdminImageFiles_AdminPosts_AdminPostID",
                        column: x => x.AdminPostID,
                        principalTable: "AdminPosts",
                        principalColumn: "AdminPostID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdminPostComments",
                columns: table => new
                {
                    AdminPostCommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPostID = table.Column<int>(type: "int", nullable: false),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminPostComments", x => x.AdminPostCommentID);
                    table.ForeignKey(
                        name: "FK_AdminPostComments_AdminPosts_AdminPostID",
                        column: x => x.AdminPostID,
                        principalTable: "AdminPosts",
                        principalColumn: "AdminPostID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Panels",
                columns: table => new
                {
                    PanelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PanelPosition = table.Column<int>(type: "int", nullable: false),
                    PanelTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanelTemplate = table.Column<int>(type: "int", nullable: false),
                    PageID = table.Column<int>(type: "int", nullable: false),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panels", x => x.PanelID);
                    table.ForeignKey(
                        name: "FK_Panels_Pages_PageID",
                        column: x => x.PageID,
                        principalTable: "Pages",
                        principalColumn: "PageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductComments",
                columns: table => new
                {
                    ProductCommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComments", x => x.ProductCommentID);
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImageFiles",
                columns: table => new
                {
                    ProductImageFileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FiePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImageFiles", x => x.ProductImageFileID);
                    table.ForeignKey(
                        name: "FK_ProductImageFiles_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    EnumPostType = table.Column<int>(type: "int", nullable: false),
                    RootID = table.Column<int>(type: "int", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanelID = table.Column<int>(type: "int", nullable: false),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Posts_Panels_PanelID",
                        column: x => x.PanelID,
                        principalTable: "Panels",
                        principalColumn: "PanelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageID", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "EnumPublicPage", "IsActive", "ModifiedBy", "ModifiedDate", "MyTenantId", "TenantContinent", "TenantCountry" },
                values: new object[,]
                {
                    { 2, null, null, null, null, 1, true, null, null, new Guid("00000001-0000-0000-0000-000000000000"), null, 1 },
                    { 3, null, null, null, null, 3, true, null, null, new Guid("00000001-0000-0000-0000-000000000000"), null, 1 },
                    { 4, null, null, null, null, 10, true, null, null, new Guid("00000001-0000-0000-0000-000000000000"), null, 1 },
                    { 5, null, null, null, null, 6, true, null, null, new Guid("00000001-0000-0000-0000-000000000000"), null, 1 },
                    { 6, null, null, null, null, 7, true, null, null, new Guid("00000001-0000-0000-0000-000000000000"), null, 1 },
                    { 7, null, null, null, null, 8, true, null, null, new Guid("00000001-0000-0000-0000-000000000000"), null, 1 },
                    { 8, null, null, null, null, 2, true, null, null, new Guid("00000001-0000-0000-0000-000000000000"), null, 1 },
                    { 9, null, null, null, null, 9, true, null, null, new Guid("00000001-0000-0000-0000-000000000000"), null, 1 },
                    { 11, null, null, null, null, 1, true, null, null, new Guid("00000002-0000-0000-0000-000000000000"), null, 1 },
                    { 12, null, null, null, null, 3, true, null, null, new Guid("00000002-0000-0000-0000-000000000000"), null, 1 },
                    { 13, null, null, null, null, 10, true, null, null, new Guid("00000002-0000-0000-0000-000000000000"), null, 1 },
                    { 14, null, null, null, null, 6, true, null, null, new Guid("00000002-0000-0000-0000-000000000000"), null, 1 },
                    { 15, null, null, null, null, 7, true, null, null, new Guid("00000002-0000-0000-0000-000000000000"), null, 1 },
                    { 16, null, null, null, null, 8, true, null, null, new Guid("00000002-0000-0000-0000-000000000000"), null, 1 },
                    { 17, null, null, null, null, 2, true, null, null, new Guid("00000002-0000-0000-0000-000000000000"), null, 1 },
                    { 18, null, null, null, null, 9, true, null, null, new Guid("00000002-0000-0000-0000-000000000000"), null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminImageFiles_AdminPostID",
                table: "AdminImageFiles",
                column: "AdminPostID");

            migrationBuilder.CreateIndex(
                name: "IX_AdminImageFiles_MyTenantId",
                table: "AdminImageFiles",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminPostComments_AdminPostID",
                table: "AdminPostComments",
                column: "AdminPostID");

            migrationBuilder.CreateIndex(
                name: "IX_AdminPostComments_MyTenantId",
                table: "AdminPostComments",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminPosts_MyTenantId",
                table: "AdminPosts",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AllowedValues_MyTenantId",
                table: "AllowedValues",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_MyTenantId",
                table: "Pages",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Panels_MyTenantId",
                table: "Panels",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Panels_PageID",
                table: "Panels",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_MyTenantId",
                table: "Posts",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PanelID",
                table: "Posts",
                column: "PanelID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_MyTenantId",
                table: "ProductComments",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductID",
                table: "ProductComments",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageFiles_MyTenantId",
                table: "ProductImageFiles",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageFiles_ProductID",
                table: "ProductImageFiles",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MyTenantId",
                table: "Products",
                column: "MyTenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminImageFiles");

            migrationBuilder.DropTable(
                name: "AdminPostComments");

            migrationBuilder.DropTable(
                name: "AllowedValues");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "ProductComments");

            migrationBuilder.DropTable(
                name: "ProductImageFiles");

            migrationBuilder.DropTable(
                name: "AdminPosts");

            migrationBuilder.DropTable(
                name: "Panels");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Pages");
        }
    }
}
