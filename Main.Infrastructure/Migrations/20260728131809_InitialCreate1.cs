using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed1335b8-6f44-4966-a05a-1fbc822e94c9", "AQAAAAIAAYagAAAAEFFBj9EIyEm6QmmrM/oNsmG1BFFIK3SaSxtD+6wMGCb2zE8h6Qdl+oGm0wP67XANwQ==", "04fd4e52-0e5b-416b-827f-7092a38d7dfb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ec9a8c4-c6b8-47b0-868f-cbc782265ef2", "AQAAAAIAAYagAAAAELD1fLiyXTZabbK5je7pRPKaAXBz83gfnFPquqUIQwCi/CPSr630MbmIw7N21VlDAA==", "4672b14a-d66b-4c53-83cf-ce203e9ecaf5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6aba32ed-d69e-437f-833c-a7a1087dd7a8", "AQAAAAIAAYagAAAAEMi10GC7ujP5hns/opplI7XuSzGN56Ym5gar0wcxPtnxbdqTVAOqLS8cN6g2z84w0A==", "935cfe7b-6f4c-43d0-ac71-741f0703ca56" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f0463c4-77f1-468a-a3f2-94c5eaacef3a", "AQAAAAIAAYagAAAAEKx+LS7xn89IjrQA809qxyZlNlFXeKqFhVK+c/BZFUkj/bgALAh9xp0uUerTD3uUwQ==", "4219c247-e093-4746-9a6b-b672d0ee0b62" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e72caad-75cd-4994-94f4-b70c59b35bc6", "AQAAAAIAAYagAAAAEAmQEpqffCFdXczttWiFf3QYDwmshMi9HFCuwl+Sdz2VD6cZF9R+2mOP6MAYipq+dw==", "21f54512-1721-4a87-a385-98c099d64ab1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "03a40d8b-6839-4690-920d-4cdc16e24d3b", "AQAAAAIAAYagAAAAEFh3paJ1xs8wVrHE8waPWnXCwB64BUiUvqiCQQdiwaNVoUTIqYMg5WcvF2LfqWkNzw==", "87f2e5b3-23dd-4b0f-8143-4c5896cf89f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a171b050-3ffc-4d41-b8d1-5b5fc7321a4a", "AQAAAAIAAYagAAAAECnoLl5dDjNmLPpOM8XODC3daGlMH0qKOHxn/WtjDeYm0OoQCeAz/Xnz3OjeUHkGqQ==", "c30d22e0-29a1-4fc2-b04b-cd7958f4c69f" });
        }
    }
}
