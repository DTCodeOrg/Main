using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "ApplicationUserRefreshTokens",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "ReplacedByToken",
                table: "ApplicationUserRefreshTokens",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7692c3a9-9658-4de1-807e-86bb0320aa37", "AQAAAAIAAYagAAAAEADFqPLP8N6HZdn2Exe/QXL9K1jXOyvYsX3gtqwyqVzXyjOQR9QwqcGM/T73DtgjvQ==", "db4354f3-641c-42c0-9aea-14868e211be2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a53eef8-37b1-42d0-aa27-d74ed3a8e9b3", "AQAAAAIAAYagAAAAEBEpJNNPvM+I5CrKTw0E3M7gPinEWMwN8hv/TJztnW+2QYC7GE/eOz9IhpYfHBg7KA==", "094c1bc6-fb11-4580-b442-696f1480b418" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56d0feb3-9262-485d-bb78-3a2479e95bf5", "AQAAAAIAAYagAAAAEEFoKtlfiTyv1PnW5KBvzScVUO9bmSySlCii8MtGTxvi5SUUI0Kuj1keUqkMhN7qVw==", "2d34099e-f142-4079-8a85-32bae1215fe3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4ccedad-d4e7-4239-a0f6-01b9551963a6", "AQAAAAIAAYagAAAAEGnU8oo9ILgxCvRAArg+zzQAhzFhCGgesuCTMb1j+jq2BAqLH5aSpm6N8MsEpY4Ijw==", "ff0bbdf2-d017-44b2-a797-f300c80af676" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbcff812-37e9-4f65-be8b-a89e2d5d4a96", "AQAAAAIAAYagAAAAEINKa8K3S/vtWCn1dBkwYkCeckBcu4QRqf6U3yKVvGfq1BE6Bb8wFfeYcRvhc+JbUQ==", "34965b6e-5e96-42c6-a0d8-e1a83d33c7be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60a35e77-8696-43d7-8e7e-932649744697", "AQAAAAIAAYagAAAAECTKY18u5/clnRi1gQ38ZJxZmrSdG2X6cNA2rmi6E9sRONZwU0TdAT12QbRpZNdLcg==", "fa0d9bf7-f077-4fca-b7f1-7912af637a07" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed2ed7b9-9a16-4f8c-a58b-740d02a16aa6", "AQAAAAIAAYagAAAAEK/nHs3q96/6Uc3O0Vmr0MBwZ5IBlMP6oFm8Hs7q49E3c7t9toMhRdae8Qdwa5yMiw==", "7254e249-e000-4959-88c9-41277b4a6c5b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "ApplicationUserRefreshTokens",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<string>(
                name: "ReplacedByToken",
                table: "ApplicationUserRefreshTokens",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ceb99bc-ac49-46de-bf75-ca187443855f", "AQAAAAIAAYagAAAAENpY9EUxn9Yz2SKqvgzQ08H5UJHfJcAX/rIIlkAvnBpyMxlR2KvOaTckCHjY5cXiFg==", "e944f625-1a01-4729-a9fe-8dee71c0f00a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eca7e9e9-01a5-4c48-85eb-7421e15910f6", "AQAAAAIAAYagAAAAEA/5+p3+AQQZD4l5lB9BIKEkCw3GTooRo9Y3qKadkaPuVRJvM0P1nGqCnwUgUUwH2g==", "1475ddd2-a344-4cf0-a31b-e7df0bd94155" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7860bc23-01a3-4b11-87a8-06a26cd33e64", "AQAAAAIAAYagAAAAEDFEhbiX3Ly+7iUFGnVrHudMvAWqCiS6xDZ2xjywgmgBMGMgpHKggGJeeSu2VJ39BA==", "d99a2d5a-7241-438c-a8a6-b32093d4475f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12244e46-3811-4597-8f78-11fd0e180797", "AQAAAAIAAYagAAAAEHhaZ4z370WyorTijCyQQcQ8Ki6PvBVHVwuBC/juVoC+/ZKyhDMXuZSIxhPYN5drKQ==", "f189b88e-9a52-4a66-a49a-3ee773e0a2cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dabae312-b977-4f5b-a914-8b50847026e1", "AQAAAAIAAYagAAAAENYpirKJZB1y0JzbnnzuxYEqwQRjNo3rs+7lIGEBG8+KhMDIKmU2wzy7S26sWOfo8g==", "2ccc3d62-02cc-4ec4-9eed-4e372741bc4a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66eda3ac-3a83-4197-bc60-7c87feffdc84", "AQAAAAIAAYagAAAAENb1mKrk6eAtTAa7pttazsq1vMxdTI51wVrOgOia39PI6i84zovCNtISgYJwPmB0Yg==", "03d692d4-774b-430f-bba3-ff19bb3fa6b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d6e2415-6df7-47ec-9ef6-137a23a86fa5", "AQAAAAIAAYagAAAAEI3FHwo9EuniMyose9rEmo5CwKkKXAGiWRy5SiMU4QeuJfCLMil8lvyDv44Uu/WOBg==", "98de4049-399e-438f-8488-bd3f53184937" });
        }
    }
}
