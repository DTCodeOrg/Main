using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TenantThemes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TenantThemes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "TenantThemes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "TenantThemes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TenantThemes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TenantThemes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TenantThemes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantContinent",
                table: "TenantThemes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantCountry",
                table: "TenantThemes",
                type: "int",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "TenantThemes",
                keyColumn: "Id",
                keyValue: new Guid("0000000e-0000-0000-0000-000000000000"),
                columns: new[] { "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "TenantContinent", "TenantCountry" },
                values: new object[] { null, null, null, null, false, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "TenantThemes",
                keyColumn: "Id",
                keyValue: new Guid("0000000f-0000-0000-0000-000000000000"),
                columns: new[] { "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "TenantContinent", "TenantCountry" },
                values: new object[] { null, null, null, null, false, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "TenantContinent",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "TenantCountry",
                table: "TenantThemes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ec18b45-7b90-4f9b-aac6-f1e1042b4537", "AQAAAAIAAYagAAAAEDbPrr2eRfg9gMH9odkhcVuETo4lj5XZVVqWd4VRucCKZtmgYbzt5qTRVBincv5EwA==", "597bc031-aa39-4096-822a-8c902fdc1518" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d168e3c-bf1a-4898-81d1-3eb6801a8626", "AQAAAAIAAYagAAAAEP8kMzanf5gOJsucUwd8hDe9D3bzFgANEuXrBfMe/9e6zfqsVNLhDvPeEM0BfszNHw==", "55b64e26-1eee-4dcc-aa82-f30494a8df82" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e76963be-72ab-469e-ba8d-adec44a0a1c7", "AQAAAAIAAYagAAAAEH/gevJFodV9G7fgc+nPaAnSqbt9IUqQrYBHjH/PWETJZ8NY7SSOolqfLVX1rP0KZA==", "baee7fe9-f316-47ad-b7d1-3b4532d5ae53" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e155c27-5a80-4da9-8875-5fccdac6da9e", "AQAAAAIAAYagAAAAEHDHBpsq1jmW6jC8nR/fDBSuJr5OSJqiQvtzsoozNyoLV4bM/yWGc8kO0TzWnpIG7Q==", "b65cf960-5385-4d69-8ca0-1d169f527cd8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a31e465-d26c-47b5-88ed-3fc4f25e0250", "AQAAAAIAAYagAAAAEDne6xQT71CK8UU4M6jD3IIs3Y3dBHBYO2pRH6l+MOVrRuM3CmuMtY+XFGRy6i0J1w==", "daf973b9-59b0-4d54-bab7-0ca41c49f48c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "452079e1-b435-4339-85c8-b02258091978", "AQAAAAIAAYagAAAAEH4U44aJKxPaUdplhxl3hKvFGQAMiVRQsMlX3aHsVCVkT/fOLO1EZS5VpxnQajRxfQ==", "53e671ee-c255-4c13-a464-f9a95a081578" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d445c9a3-6b8d-4e7d-9153-fd2b5aaead17", "AQAAAAIAAYagAAAAEB3paxZTN4QQ6ORJR50ndI9FUf2YLH7MTfSv+Xmad611scflWj/cYoYD/aaUt4mo8Q==", "606b2c1a-5c7b-4fc9-96fa-d56c3e850c9f" });
        }
    }
}
