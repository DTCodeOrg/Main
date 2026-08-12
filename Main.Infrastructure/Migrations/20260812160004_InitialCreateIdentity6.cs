using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRefreshTokens_AspNetUsers_UserId",
                table: "ApplicationUserRefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUserRoles_AspNetUsers_UserId",
                table: "TenantUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "ApplicationUsers");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_Email",
                table: "ApplicationUsers",
                newName: "IX_ApplicationUsers_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e36cad19-655a-4ce7-a1b8-2c4c739d7b9d", "AQAAAAIAAYagAAAAEJcrrYsDDmMJuV0Px4g+vET6XxfETXGqWM9Ma8YfRTAKqkjKBy1chPSh82j5vK9mLA==", "683e1923-d353-43c5-93a1-6a87f54dad4d" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d0264a8-0bf9-4e6a-9b2b-84bdf6c706e1", "AQAAAAIAAYagAAAAEIj1fidiPOeFrjjmgLiL/3Nz886yzr/ChyJ2TuM8kbbDvIeYlkhxjwdDmFCP5RRFcQ==", "d75f3e6e-189a-48d1-8d05-51c01fa8c7b6" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c03437c-2235-48ea-83a3-7c9a6b96f259", "AQAAAAIAAYagAAAAEFAH4hDrvFNV+f2ALkOrupLLI7HXejR32U/lDKRm7zR7jvgIw8Td5fEaGYCgNwTGtQ==", "6f36261e-983c-4412-8460-4e524d5b44a7" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bad59c9c-6135-45aa-9cce-e391b75f4678", "AQAAAAIAAYagAAAAEHpb0my3YiaPxqxAyJ7gkg7OEGsWmcoSZk+apSCA3j5OZp3uyIcBvJBjY2zpLSBCWg==", "81c7c552-b42a-4241-8302-070cc2185bcb" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5eb22aa2-bfb9-429d-9767-3f20f0141743", "AQAAAAIAAYagAAAAECssLyj/GPfueHi71SmYsE7g2tEyIq31HzeAW2oDMiC9lLKXq5Z6dyz9oiqMUtGnBw==", "e9fbb6e0-bb98-4a33-a8b7-eefd559c96dd" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6de48178-06ed-4ce4-bfee-56102211a231", "AQAAAAIAAYagAAAAEEYo9AnauYwyxU1pHZVzyAHIVKkif8z9zF4W9TE+NBMyrw+ewPbCGMSLzfxsIJMMvA==", "297f1b3b-33a0-410d-bb6d-821295ba669e" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c6b2193-bd0a-4098-adf6-413a7b8bda52", "AQAAAAIAAYagAAAAEOV4KuUdmT2K0s7UWJMvaWmqI2B/Ev3K3qD6F/p0U+celyUfZTXEkphYojT8VogkhA==", "c0bc64c0-3494-496c-bb84-873e347afe3a" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRefreshTokens_ApplicationUsers_UserId",
                table: "ApplicationUserRefreshTokens",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_ApplicationUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_ApplicationUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_ApplicationUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_ApplicationUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUserRoles_ApplicationUsers_UserId",
                table: "TenantUserRoles",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRefreshTokens_ApplicationUsers_UserId",
                table: "ApplicationUserRefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_ApplicationUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_ApplicationUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_ApplicationUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_ApplicationUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUserRoles_ApplicationUsers_UserId",
                table: "TenantUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers");

            migrationBuilder.RenameTable(
                name: "ApplicationUsers",
                newName: "AspNetUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsers_Email",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a3e473f-2b01-4bdf-9106-81282305045e", "AQAAAAIAAYagAAAAEA5nwgEBCW+hvNRelt7RRyUlk3JAHBpmBGefwljpMWxPN9685zfhGdipiIqOj2mlkg==", "2153284c-4d49-45af-ba78-53e8c2cc562a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "457a22f2-976c-4def-bebf-55104cb4b9ae", "AQAAAAIAAYagAAAAEKrhy49E7Jlv3kZSzFJA31FYDBF+zCBHpHoyL6VoI2KTkBPtq4kMRD9xwB+xPdqxQA==", "77641c2a-847a-42d5-8465-ac8f4f5707c5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a58993fd-64ab-4f62-8e25-5b56da06fdd6", "AQAAAAIAAYagAAAAEK/e5mDjCdhYzxT1xJSttpIP9zb50WbA8VERk3/g7jkT5QJYUv4r6yNipeKpVMXeNA==", "79be1eb4-89e7-458b-93ae-049a22ea66b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df27fabe-ed5e-435a-aa61-3e3645c3c52d", "AQAAAAIAAYagAAAAEK99deENO1pxJyHoy1BmRNhPGIDPVyJf4sosdBA5la4735g5DyEz7leyIvcWo9v/3g==", "e21df732-7934-4712-9b7f-6b17b1cfb87b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "858948bf-966a-4220-9a8f-dbf32c72db7c", "AQAAAAIAAYagAAAAEL2gLwGBMVUsSr8AAwd+yXmGqw7FDtHhRTryEWwLGx5/TK91Mpr+i3T1JCmtQ3rpWA==", "d7d55676-1652-4e8b-a27a-f03b6dc1e018" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c7a6067-aba7-4e26-b141-929d8aa152ab", "AQAAAAIAAYagAAAAEA83vbiEQ3uMgN+02j8i0AkxzVAwGLejPjEh2O3vas1x1owDc+uT3c66uf33aBFYtA==", "dd6088d5-691a-4c51-8d0a-38106dfec605" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f32cd1c-69d4-4729-b663-106dc2ffaf7d", "AQAAAAIAAYagAAAAECk1CvLidyBG4xQFJE/PxPI5znuQgQDZW3yXaStaC1HX0n1UMc/JE54LLvotdJu2Eg==", "310cdcf2-8f8a-4fa4-a733-f83beaa8d26d" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRefreshTokens_AspNetUsers_UserId",
                table: "ApplicationUserRefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUserRoles_AspNetUsers_UserId",
                table: "TenantUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
