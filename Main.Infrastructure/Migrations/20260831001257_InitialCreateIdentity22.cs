using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000003-0000-0000-0000-000000000000", "00000005-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000004-0000-0000-0000-000000000000", "00000006-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000004-0000-0000-0000-000000000000", "00000007-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000004-0000-0000-0000-000000000000", "00000008-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000004-0000-0000-0000-000000000000", "00000009-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000004-0000-0000-0000-000000000000", "0000000a-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000004-0000-0000-0000-000000000000", "0000000b-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000");

            migrationBuilder.AddColumn<int>(
                name: "StoreType",
                table: "Tenants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00000013-0000-0000-0000-000000000000", null, "GlobalAdmin", "GLOBALADMIN" },
                    { "00000014-0000-0000-0000-000000000000", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "fab21e4f-50c7-4617-bf68-55fa6075beb8", "tenant1.manager@test.com", "AQAAAAIAAYagAAAAEJ/Zo38Yw4Ujgqm4BiOl4aRRM6nh0mpZNkfkZPgY2D338OHtNMCwkRC8/2rqfM69LQ==", "efad81c1-1204-4cd9-909a-e355f71f854a", "tenant1.manager@test.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "c9e66ae4-c503-4f4d-86e8-bcfe04c33af7", "tenant1.member@test.com", "AQAAAAIAAYagAAAAEHRxbl2CAZXBkF+F4t5yjIGTqafDFA2Sook4U45kHcnNGz9pUOeWR1HL/NGRZTSICQ==", "84ac87b0-dac5-4d0f-b8db-7aa24f59eea6", "tenant1.member@test.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "4622912e-4bda-4c5a-b3da-8dc3cabc495e", "tenant2.admin@test.com", "AQAAAAIAAYagAAAAEFJrsxE6cXYQAUSfksHjKnnm6YxpKuI0sJgvfm/ftk/amg+eg6EnhvDsJv+gtIKBTw==", "782ebdaa-916c-4da3-888f-73011122c8e9", "tenant2.admin@test.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b975e7df-0933-4a28-b754-c966caf1788a", "tenant2.manager@test.com", "AQAAAAIAAYagAAAAELwaErHt5+WONW4QK7DgCN4i4UlVkihqJ4d+YPM8QwzmiP61YGaf5t6CM1HbURDebg==", "54ae93dd-01df-4e78-b7bf-d63392ae579f", "tenant2.manager@test.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "d571ad77-6194-453f-9533-9e5aa8756384", "tenant2.member@test.com", "AQAAAAIAAYagAAAAEPiAWFhIXDXvlVDRpmIn5QWuDx+X9KBAgeK6lsEqmWxZhRZ3BdbctQpda8vNCv5hQQ==", "dd5d5920-6fc2-4c1d-a9d8-954778fc567d", "tenant2.member@test.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "9257295f-9a97-4b9f-9d5b-e5a084912cbf", "finearts@test.com", "AQAAAAIAAYagAAAAEItMAgl3zjruDdA94SvQqt1IIQVbhGOGeuscdmxTF/EQj/aboYYxxm8BuN6g7qm0PA==", "90b2f505-9837-43d3-8f6d-2ec991288eb5", "finearts@test.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "e2f0b468-2979-4bc1-ab13-c9f5e5cccfa9", "lifestyles@test.com", "AQAAAAIAAYagAAAAEDNCF6Sw9zmoCRLT/z5lPXmmjr93b32XySOC1pUPbsMweEFbThrLE2DPjWmcIBIwJA==", "2d0e228e-9129-4062-a200-3f18146a82d2", "lifestyles@test.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00000003-0000-0000-0000-000000000000", 0, "21d4283f-bf32-48d8-a244-364ada042e05", "admin@system.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEGt6TFQV0uZi7iiWMKSN9Ez+S4Z7D8DBonnKeJwu2n+WOsAKiCOT1lnhPoYFit9lsA==", null, false, "0d32b4ff-3854-4eeb-984c-3afa5513ee81", false, "admin@system.com" },
                    { "00000004-0000-0000-0000-000000000000", 0, "37392816-2b40-462c-832b-bfcc0e1ede3f", "tenant1.admin@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEFW2ldkGpfI/Kdk0ba6fFdWCL64NCNA4TyH4oM1C57UFdZFkyvR5hFi4FYqW4XxAgA==", null, false, "d5ebf779-39ba-40c5-97fd-972544140174", false, "tenant1.admin@test.com" }
                });

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 2,
                column: "UserId",
                value: "00000004-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 3,
                column: "UserId",
                value: "00000005-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 4,
                column: "UserId",
                value: "00000006-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 5,
                column: "UserId",
                value: "00000007-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 6,
                column: "UserId",
                value: "00000008-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 7,
                column: "UserId",
                value: "00000009-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"),
                columns: new[] { "HostType", "StoreType", "TenantContinent", "TenantCountry", "TenantName" },
                values: new object[] { 1, 6, "Asia", 1, "Tenant 1 (Finearts: Collections)" });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"),
                columns: new[] { "HostType", "StoreType", "TenantContinent", "TenantCountry", "TenantName" },
                values: new object[] { 1, 5, "Asia", 1, "Tenant 2 (Finearts: Crafts)" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Host", "HostType", "IsActive", "ModifiedBy", "ModifiedDate", "SecretKey", "StoreType", "TenantContinent", "TenantCountry", "TenantName" },
                values: new object[,]
                {
                    { new Guid("00000011-0000-0000-0000-000000000000"), null, null, null, null, "fiearts", 1, true, null, null, null, 2, "Asia", 1, "Tenant 3 (Finearts: Arts)" },
                    { new Guid("00000012-0000-0000-0000-000000000000"), null, null, null, null, "lifestyles", 1, true, null, null, null, 1, "Asia", 1, "Tenant 4 (LifeStyles)" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "00000013-0000-0000-0000-000000000000", "00000003-0000-0000-0000-000000000000" },
                    { "00000014-0000-0000-0000-000000000000", "00000004-0000-0000-0000-000000000000" },
                    { "00000014-0000-0000-0000-000000000000", "00000005-0000-0000-0000-000000000000" },
                    { "00000014-0000-0000-0000-000000000000", "00000006-0000-0000-0000-000000000000" },
                    { "00000014-0000-0000-0000-000000000000", "00000007-0000-0000-0000-000000000000" },
                    { "00000014-0000-0000-0000-000000000000", "00000008-0000-0000-0000-000000000000" },
                    { "00000014-0000-0000-0000-000000000000", "00000009-0000-0000-0000-000000000000" },
                    { "00000014-0000-0000-0000-000000000000", "0000000a-0000-0000-0000-000000000000" },
                    { "00000014-0000-0000-0000-000000000000", "0000000b-0000-0000-0000-000000000000" }
                });

            migrationBuilder.InsertData(
                table: "TenantUserRoles",
                columns: new[] { "TenantUserRoleId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "TenantContinent", "TenantCountry", "TenantId", "TenantRole", "UserId" },
                values: new object[,]
                {
                    { 8, null, null, null, null, true, null, null, null, 1, new Guid("00000011-0000-0000-0000-000000000000"), "Admin", "0000000a-0000-0000-0000-000000000000" },
                    { 9, null, null, null, null, true, null, null, null, 1, new Guid("00000012-0000-0000-0000-000000000000"), "Admin", "0000000b-0000-0000-0000-000000000000" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000013-0000-0000-0000-000000000000", "00000003-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000014-0000-0000-0000-000000000000", "00000004-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000014-0000-0000-0000-000000000000", "00000005-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000014-0000-0000-0000-000000000000", "00000006-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000014-0000-0000-0000-000000000000", "00000007-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000014-0000-0000-0000-000000000000", "00000008-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000014-0000-0000-0000-000000000000", "00000009-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000014-0000-0000-0000-000000000000", "0000000a-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "00000014-0000-0000-0000-000000000000", "0000000b-0000-0000-0000-000000000000" });

            migrationBuilder.DeleteData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00000013-0000-0000-0000-000000000000");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00000014-0000-0000-0000-000000000000");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000");

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000011-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000012-0000-0000-0000-000000000000"));

            migrationBuilder.DropColumn(
                name: "StoreType",
                table: "Tenants");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00000003-0000-0000-0000-000000000000", null, "GlobalAdmin", "GLOBALADMIN" },
                    { "00000004-0000-0000-0000-000000000000", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "c651deaf-55fd-4180-a6ca-80a154b57c4c", "admin@system.com", "AQAAAAIAAYagAAAAELtZj4mnKOjYDyWc4DScBxI97m3PWelVIDLaJhJHWSpTlY82oU8iGrs4Yu1pPtSnww==", "9ec808b2-1fe0-49cd-8325-7f14d1f38a20", "admin@system.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "c7de4f6a-cd5c-49e9-88e8-d0be84260260", "tenant1.admin@test.com", "AQAAAAIAAYagAAAAEPO/uirdrZqkwbcFbidbP0wXH8cFx0Sk5brCwweESR3S64dUA4FhdxRfqdd95DQrPQ==", "1ad289cb-8928-491e-bb89-a2a69138da79", "tenant1.admin@test.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "3e848d85-5f5d-4887-9e54-6e80a2d4ce83", "tenant1.manager@test.com", "AQAAAAIAAYagAAAAEDIftjWK6EoGvCLQxv7GhUc2bgnXn8mSmV2VeBCbO8sYdgIJwItWJpESqIpFc48yRg==", "663a7b86-fa79-450c-a961-84ac12ec93f3", "tenant1.manager@test.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "20fe587b-160d-4bd6-b77e-c4997e054d76", "tenant1.member@test.com", "AQAAAAIAAYagAAAAEKpZv3PTzpqGaV22XOWdB1ZyYOqSn5KS5idt49ol7MS5Id3/i1PyaSzrROUHYXFjww==", "b2104df7-2f64-42de-a70d-e16b6c94581f", "tenant1.member@test.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "102876b7-a286-44ff-9ab7-b402f90499e6", "tenant2.admin@test.com", "AQAAAAIAAYagAAAAEMYSzy5pwWfW56OUSvka26c9R/yRKmQYoltJ7xReZumlPbVepiqDcT17OehrM/KVMA==", "8109755a-d1b4-4403-ab1b-43c0d3dc711f", "tenant2.admin@test.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "eabbe2e9-a6ab-4541-bb8c-bc339796b575", "tenant2.manager@test.com", "AQAAAAIAAYagAAAAEE+aG8Nloca4/uq4S/7UdIeSfG1e4NjdKgN/oj68DQ9Z5SG3Wldtrc1UDnR/nvNfJg==", "bd47d479-87ef-4395-81d9-78fbd7809f9e", "tenant2.manager@test.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b7931f45-1a6f-434b-96e8-34584e40ff19", "tenant2.member@test.com", "AQAAAAIAAYagAAAAEFV7DhRzSSouSCqLcS1hmcd0Zu53KizV5ViAVG6izFQrLGk2KyaCremg7dFek5iuGw==", "e642887e-20f0-414a-b12a-610aa4a6ffc8", "tenant2.member@test.com" });

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 2,
                column: "UserId",
                value: "00000006-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 3,
                column: "UserId",
                value: "00000007-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 4,
                column: "UserId",
                value: "00000008-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 5,
                column: "UserId",
                value: "00000009-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 6,
                column: "UserId",
                value: "0000000a-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "TenantUserRoles",
                keyColumn: "TenantUserRoleId",
                keyValue: 7,
                column: "UserId",
                value: "0000000b-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"),
                columns: new[] { "HostType", "TenantContinent", "TenantCountry", "TenantName" },
                values: new object[] { 0, null, null, "Tenant 1" });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"),
                columns: new[] { "HostType", "TenantContinent", "TenantCountry", "TenantName" },
                values: new object[] { 0, null, null, "Tenant 2" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "00000003-0000-0000-0000-000000000000", "00000005-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "00000006-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "00000007-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "00000008-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "00000009-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "0000000a-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "0000000b-0000-0000-0000-000000000000" }
                });
        }
    }
}
