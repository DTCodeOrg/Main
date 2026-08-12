using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ApplicationUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "ApplicationUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ApplicationUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ApplicationUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantContinent",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantCountry",
                table: "ApplicationUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "PasswordHash", "SecurityStamp", "TenantContinent", "TenantCountry" },
                values: new object[] { "f0a7174e-9b9c-4033-929b-239481ca1540", null, null, null, null, false, null, null, "AQAAAAIAAYagAAAAEEqj/5WLNcYbunYnwlFqV9DpJZ8uW04RKdGCsgYEcMfFv4vfwhP3z/NJIm4PBLlm6Q==", "8284e5b4-5b31-4a8f-9c43-6493665d56da", null, null });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "PasswordHash", "SecurityStamp", "TenantContinent", "TenantCountry" },
                values: new object[] { "34fb6a41-0cf3-443b-9590-310cde56993d", null, null, null, null, false, null, null, "AQAAAAIAAYagAAAAEBy45ZIvek13bOecn6GjMlpnC+riD5i+bGYGC4sP/lmlhtYMQuZPort6T6yDxAcJ4g==", "dbeef992-7b34-4879-8f74-0a0499ed2965", null, null });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "PasswordHash", "SecurityStamp", "TenantContinent", "TenantCountry" },
                values: new object[] { "71ce771d-78cf-487d-a9ba-d89a755109f8", null, null, null, null, false, null, null, "AQAAAAIAAYagAAAAEEyIsglrW3Dg6/8qKPpJV7NcNhlzj0U7cMcjQAZyiSTaksmnm92CVQN8umsYPqVHuA==", "d3f1327f-2b29-4297-bda4-a68656fa9feb", null, null });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "PasswordHash", "SecurityStamp", "TenantContinent", "TenantCountry" },
                values: new object[] { "5953ddcf-68ec-43b2-8e56-0b1a5e552f7e", null, null, null, null, false, null, null, "AQAAAAIAAYagAAAAECfZBzasMGangNclQUIWQ1eFd+YT5aI0yr0gm2GmQdpJOAkoEzlLml039RzH0H7UmQ==", "a57f9dbe-1285-4b39-940d-fa0916448d5c", null, null });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "PasswordHash", "SecurityStamp", "TenantContinent", "TenantCountry" },
                values: new object[] { "459ce0cc-451b-4cf4-bd3b-4b61df4bd174", null, null, null, null, false, null, null, "AQAAAAIAAYagAAAAEGy1ieIrrfQ9kz4m0mIr+3v/3FKldg94jPx9mcbOGwVf4K34fW+vR+E/WZcJdaHi7Q==", "4df51b12-e76a-49fc-8017-e5f98c092a45", null, null });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "PasswordHash", "SecurityStamp", "TenantContinent", "TenantCountry" },
                values: new object[] { "12c4e885-18ba-4796-be3a-c41402bb2d55", null, null, null, null, false, null, null, "AQAAAAIAAYagAAAAEF+TUOu2ySEbFEEHnhc2Q9dTCo+mu+ZvRTOiQ+4IzSGwX3fkxU1UCBI65Y285S/6gA==", "adc7cad4-ae2b-4d50-80b7-871ead5b3fcf", null, null });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "PasswordHash", "SecurityStamp", "TenantContinent", "TenantCountry" },
                values: new object[] { "c39b49bb-6010-4f7a-8598-02bfa7d7b9dc", null, null, null, null, false, null, null, "AQAAAAIAAYagAAAAEOZheZeqx1b3gdHV7Uzb6sJMtFnZxRpnK+7M4S+SZW+JxLgK9P2Vllh+jTRzNo3rlA==", "905d6bf9-439d-402c-9965-e8da7d705268", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "TenantContinent",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "TenantCountry",
                table: "ApplicationUsers");

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
        }
    }
}
