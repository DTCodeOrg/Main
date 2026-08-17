using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondaryColor",
                table: "TenantThemes",
                newName: "MenuItemHoverColor");

            migrationBuilder.RenameColumn(
                name: "PrimaryColor",
                table: "TenantThemes",
                newName: "MenuItemHoverBGColor");

            migrationBuilder.RenameColumn(
                name: "BackgroundColor",
                table: "TenantThemes",
                newName: "MenuBackgroundColor");

            migrationBuilder.AddColumn<string>(
                name: "BodyBackgroundColor",
                table: "TenantThemes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BodyColor",
                table: "TenantThemes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ButtonBGBorderColor",
                table: "TenantThemes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderColor",
                table: "TenantThemes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoColor",
                table: "TenantThemes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2eca9aa-19a8-4712-88ed-e02549b3bcee", "AQAAAAIAAYagAAAAED2Qx93jDLF+JrDlWv18UPmQSgG1lp+N/3B9OTtY8NGz5DHwSicbY5a139s1PrAzjQ==", "a5547d16-219c-4d03-8159-82b85404de48" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de6afe88-bbd2-40ae-a942-ab08d70af99b", "AQAAAAIAAYagAAAAEMGus4iUzQHXHR5jFYSZDqvLKRkoBkioXLuQQAMA+PMy23JNnsJjWZIIQHGMO7sH9w==", "08947f6f-1a4e-4483-8def-2a922f1f2155" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c343f8a0-23cb-48c7-a770-19fdd22ff308", "AQAAAAIAAYagAAAAENXG5pEZHmPpsax/X/QZtVXGg6QSZMX5DElF0kIrSyRhr0ZFtFTMsY3ldOGyZPjvTQ==", "460d2567-4461-4502-91ad-7a08c19ea0e3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b4cc0c3-e23b-46f7-99ed-bd884f0bde55", "AQAAAAIAAYagAAAAEIyOw7CTwyLN195v/WiOWcYscb5Bov3AMFHuLZk3fp7BcSufMdY3TCEwyYBMVa847A==", "95df8190-ecf0-4085-b91a-f71591533e08" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80999a9b-58f6-44c7-b0a8-3bd296d3b720", "AQAAAAIAAYagAAAAEDyyd8DAlDlxBP7UgSsyCPDuD+cviq6QIzhkZZKnvh8UdBjJOvsQBZepZW9f2ywPHA==", "ab29ce73-4f09-4e58-8187-2ce7636bd790" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50d9e9ef-894d-41a4-bcea-b94bde6e63e7", "AQAAAAIAAYagAAAAEMhi0C5Uvpp0D/UzbIReou6IKiHUPkC51BAT86CNPBt/nuH0EybfUmEvLU2LADYOVQ==", "aa19674c-4906-4b5a-b937-7cedb034349b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08da5486-b466-4a0d-9fd4-1006b4ece9ba", "AQAAAAIAAYagAAAAEDHo/Vv7abLV+/F1JtSgPTnMILcq9wNrEI5ddoETZZEtYEL8ohSO1KRHQPxDb0t5GA==", "ec281ccf-140c-4d75-9000-04cf69d0b952" });

            migrationBuilder.UpdateData(
                table: "TenantThemes",
                keyColumn: "Id",
                keyValue: new Guid("00000011-0000-0000-0000-000000000000"),
                columns: new[] { "BodyBackgroundColor", "BodyColor", "ButtonBGBorderColor", "HeaderColor", "LogoColor", "MenuBackgroundColor", "MenuItemHoverBGColor", "MenuItemHoverColor" },
                values: new object[] { "", "", "", "", "", "", "", "" });

            migrationBuilder.UpdateData(
                table: "TenantThemes",
                keyColumn: "Id",
                keyValue: new Guid("00000012-0000-0000-0000-000000000000"),
                columns: new[] { "BodyBackgroundColor", "BodyColor", "ButtonBGBorderColor", "HeaderColor", "LogoColor", "MenuBackgroundColor", "MenuItemHoverBGColor", "MenuItemHoverColor" },
                values: new object[] { "", "", "", "", "", "", "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyBackgroundColor",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "BodyColor",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "ButtonBGBorderColor",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "HeaderColor",
                table: "TenantThemes");

            migrationBuilder.DropColumn(
                name: "LogoColor",
                table: "TenantThemes");

            migrationBuilder.RenameColumn(
                name: "MenuItemHoverColor",
                table: "TenantThemes",
                newName: "SecondaryColor");

            migrationBuilder.RenameColumn(
                name: "MenuItemHoverBGColor",
                table: "TenantThemes",
                newName: "PrimaryColor");

            migrationBuilder.RenameColumn(
                name: "MenuBackgroundColor",
                table: "TenantThemes",
                newName: "BackgroundColor");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "19a68eeb-b356-4212-a138-13bbd3967a45", "AQAAAAIAAYagAAAAECYzOmeAj1JYa4RT86L7DklXV2SMKkUwGLp2TEWDEqPiiEwhjyhYtKvsubiXjkfHtA==", "5dc328c4-6331-4495-a8e3-4903ce1ff7b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b243723-278e-4ef4-bbc1-7fbf461b0409", "AQAAAAIAAYagAAAAEDwffRIND3DruzsimqGlD/gUnDqXlJUz/nrrpKAvolLtZhQGci9V1wEFDHWedi7k/w==", "3a25aaa7-1d19-4b65-933b-111452a0c8f0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ac1bc0b-6262-46f5-aba0-7ad3ad4d06e4", "AQAAAAIAAYagAAAAEPekZ74bAqneOMK55TmECmX9QP8FXEFgV5RcG6+JIHM5KPSJ9//dJxHHroIkAAJMyw==", "51304a0a-06e1-4942-ad4d-b112dc1e4228" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47d7e27a-8884-4c2c-bae4-342a288c9546", "AQAAAAIAAYagAAAAEKYtHfukWDtniJ4/M59eJvj/SoCqKiypufyHOVCSSnHOJ/XfYt5U10DT7szosFPrsA==", "939112fb-3ed1-4221-915f-99f743ae053e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28507262-ef7f-412d-9ec6-c9a8dd4ec8ff", "AQAAAAIAAYagAAAAEMOInq4M6hNHbiHue5YoACduDPkNkO29ZZ6psIDqhHOXHlXD/jHU8iDAzdpEoSCLbQ==", "1c139275-0ebd-4053-a8bd-7bd46bfe6e66" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "135f575f-27e9-40b1-98ca-7efbd1426b44", "AQAAAAIAAYagAAAAEDKAN7uRV3OfVZFNI4CwOo7uzF7qzNOelqncvDldkG7M0ECYSIYsTHYFghPYlGskNQ==", "8b9ceda6-2377-456c-a01e-3e2843f71acc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "754b2ed1-7ddf-4996-b9d3-08489ba31910", "AQAAAAIAAYagAAAAEOgfwloC+KR9ahOqY94F3zzKlHy1llopbDVKnALQSFf+2jh9pp7Xg3MfNnvewGLFWg==", "8ebf4155-1b07-443d-9c4a-de05ccf4f912" });

            migrationBuilder.UpdateData(
                table: "TenantThemes",
                keyColumn: "Id",
                keyValue: new Guid("00000011-0000-0000-0000-000000000000"),
                columns: new[] { "BackgroundColor", "PrimaryColor", "SecondaryColor" },
                values: new object[] { "#F7F8F5", "#122A1E", "#879882" });

            migrationBuilder.UpdateData(
                table: "TenantThemes",
                keyColumn: "Id",
                keyValue: new Guid("00000012-0000-0000-0000-000000000000"),
                columns: new[] { "BackgroundColor", "PrimaryColor", "SecondaryColor" },
                values: new object[] { "#F4F6F4", "#1B3B2B", "#728C69" });
        }
    }
}
