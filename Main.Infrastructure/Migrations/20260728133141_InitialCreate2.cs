using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bb96658-8fcf-417d-b975-e7db4367587d", "AQAAAAIAAYagAAAAECc0ZNjtszknpEq42burnGMuDF863y94s4JXWrCQ/etCpPyj4/yzx7/9ykfDd8Fjmg==", "0ea0e2b0-cc61-480c-a4c3-80e6fbade52c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91cb1911-7ffe-4205-8bec-0373c52e89ec", "AQAAAAIAAYagAAAAEEV1xUiRPPxBjNhzGImQW7A4r6xcFXoMkBgCfzqmy4rQlZEEnK7fk6nrahZ+tPpISA==", "d40087e7-7528-4a2b-a4af-7c047f3f07e5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a758b0f-31a5-40be-9a3f-eb40a8974e66", "AQAAAAIAAYagAAAAEJtWtMRYj0IupNpLIh4TZ0koTDgXmxsHNjxbxzdIVwpdWoUKq1MpViJ6JjHo3ux8Sw==", "d9da9dc6-ef3c-4450-9915-eeac053fba2d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b1fbd45-db9c-44b7-a3f0-45228a8a1922", "AQAAAAIAAYagAAAAECA2Gq/0aOOoLXwl6J1FL85co0wpCx6fS7M2GMuPfXErEPSqEzCcoULzQc6hfLYj0Q==", "4d2ce252-bb47-4487-95c1-5eaa45f743c3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59f5cb4d-3585-4e9e-9a4d-52a3dd1ee2c8", "AQAAAAIAAYagAAAAEE+psQEYjtOjRhsxMf13mIopv98ha2VZc7mpnuH5/gxh3wL5habiGoyGbFNDY6+j0w==", "b456a8b7-0aee-4f6b-ab19-8ea5f5bea762" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1e83701-1c09-4986-8d24-93f35cf244ee", "AQAAAAIAAYagAAAAEB8t0n6FAqD2KYqDvzT0puh4OxdpAKtAjry+uJx0yk8bJqgKyoH5fcc1zXdReshyrw==", "eaafaca9-6fa7-409f-9133-a7b5c1b5116c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47b877f1-7be8-4ea0-95a3-7c0199bc454f", "AQAAAAIAAYagAAAAEOoUkT/zNwTgKy2DRzKXOUVbqD1S8NaQOnkXzPverqBbzm57nn3keihzkc2+4KmmDQ==", "76926d91-e62b-46fe-bf93-006241b16a7e" });
        }
    }
}
