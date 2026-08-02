using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRefreshTokens_MyTenantId",
                table: "UserRefreshTokens");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e77a89f3-5657-489d-ab9a-3c3f1bef0d5f", "AQAAAAIAAYagAAAAEDGdFAW6Bj1HYzNC0bFXFYhK7t0ii+KF3URcWyteF+WY0+BnmLFDjCmmzWlKToSg+A==", "a08907d4-f145-4ba4-8ba8-232e5d68f51e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "047404f7-c893-4dbe-9cc2-2edd00c3f9c8", "AQAAAAIAAYagAAAAELddlAi+ng1vZlcAb6XzFReJUdBeaLntJU/vkzJqowCGYjzayUkCd7GSqq7oTASl1A==", "a4e4fd60-0e68-4ce5-be98-ef93749fa114" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7b8df92-b5c2-4dee-935d-c6c56824a33f", "AQAAAAIAAYagAAAAECRBrWEnpB8gmgOLJ6XI7I/8QwJjV0hCaelSUYM5PKYL/qurWnm9p0E9s1v+ld4kjw==", "66889a08-e58c-4529-9465-b090246e6f05" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e1e64b0-63d7-4a19-a47a-3b5214ef9cfa", "AQAAAAIAAYagAAAAELiInnZAH/6Zo+4fKx1U6RSU6/xp6Ojl1iXMIVgdWTvwIbbL+FZ2a5MPOuIMHgYB0g==", "17e6e005-d57a-40f2-94ce-275049482849" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85f45b7f-e017-4732-9dc0-1e83f5b045b7", "AQAAAAIAAYagAAAAEJsGDN3M6DdHjKcrBSjoGjSY8NS7pm/MXTeFyewGgXJljMUKaqTLYWo9dpJJZwkNNw==", "057e3561-c0bd-4377-8e1a-d33bb99cf38d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1985f6a-4e08-4ab9-9c57-2f38a29d6a5f", "AQAAAAIAAYagAAAAED3YMZFIEPqmcpw71lk16FZPf5fkaiwLSOi+kfBs1CemKRdVvN87BQrNX6LCsksCkA==", "dbf2f8d5-77b8-4be1-abfa-d4d3d72cdcd0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a67c8c5-1b70-4d55-bf9b-02b5a697bf44", "AQAAAAIAAYagAAAAEAcqboTyazO3HE78fSJ0ax/UAVLVpyWZZrd1OfG3RQZ/XLVyqNG9W3wGul/C4lfb5g==", "5e543f27-366d-4dfc-a943-3dfcdcf7caf0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8cdf8be-dee9-41a2-9c7a-5b5faf352c69", "AQAAAAIAAYagAAAAECt4ZcF28d3dA04IQQaSK6JKQuGUaOE79DAYFyw3WjN5033sGKYIkxKc5XZQ4cUCug==", "14a5c98b-48bb-455f-b787-e0e63f9876fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49c0c91b-c2b7-4b65-9b5a-2e73d2211a49", "AQAAAAIAAYagAAAAECGH3pnx6PAmq4WO/9mITo+wfkhpPhBOOskrfvkrMKa/nowxKAubwLQ3LXZVRYIRww==", "fb0dda0e-4df7-430d-9243-12d129c1aa22" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fcf094c-cf29-44f9-bda3-091ade04c9fc", "AQAAAAIAAYagAAAAEIpE0QQQ9W79URMz9sZ+oWqgU+TWl4VDHdIJ0GHlInU0zok8FQL+ejui+YEOwtpeBw==", "029efc6e-ebd3-4af4-a115-e298e660fbce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c19877a1-d40b-42d9-a0e0-4171070455fe", "AQAAAAIAAYagAAAAEFJiroSf2o7QcTvIuKHHneKW1FGY/0QzudyHB/nG2EAQC/s+BZL8LnLhwF08QUJfjg==", "9205484a-944a-4e47-88b5-8a388e40ca3e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f87f13a7-003f-4c96-a37d-f5c5d77544da", "AQAAAAIAAYagAAAAEEOaLcN+anlEZeGnwgfA6APMI45z1LiWpGcErxEWITuss85gwwtwmjhCrZSnBw5ZYw==", "ccfe9e08-4a68-4f97-80a2-87224fa6e29b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5904b420-6553-474a-9715-eb79342b6795", "AQAAAAIAAYagAAAAEMK1PpSnuetnnlkB+qaJMwVyP9Y38/DzdIqDlRcvNhKOCg/tWD2ml05FqHaST8rVwg==", "face1ecb-d5a7-4b9c-b007-09e7f0f62cf2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3287f1ee-61bc-4434-8245-01b245879881", "AQAAAAIAAYagAAAAEF+ero+XHBEMYezok3EKYVmJqUkpx1VGmn2RZyklqIpNzmwoWD6dJ18Bdw5Nouq01A==", "6e5a79ed-85c7-4e76-a6e9-cbfc7912a298" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_MyTenantId",
                table: "UserRefreshTokens",
                column: "MyTenantId");
        }
    }
}
