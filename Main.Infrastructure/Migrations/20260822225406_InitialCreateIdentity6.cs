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
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45a39275-2ec8-43fa-86d2-0d1c1a703fd1", "AQAAAAIAAYagAAAAEABN/X/hY+ocqK8HakVLaxCKvnebipyptrIUYief1Es71QftOtO4L5rtmX3VKsb4IA==", "e4f0acd9-4210-4546-ba55-a147ce72215b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb104c22-30a0-4cdf-aad0-2d9617ff8f1a", "AQAAAAIAAYagAAAAEEMvgaSSLaZVxQweYgfU5YvMIafGyhj0LFt9QajqdTblAHYGj9sVDKOJ2rM50MqtZA==", "2c0195af-0208-45ed-8738-0edccba94128" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13603590-838a-4216-b8e6-5a5ea6d6027c", "AQAAAAIAAYagAAAAEH+LT2q6gPevzYM5Gdm8VUSxSBUtx71lHlYaLmelM4RpFYzpCbBS5UcX0cJ4xdb69A==", "2d813792-1caf-4438-b7d5-b7b5bafa412c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c2312c2-49a1-446e-8fd1-788723dc5cd2", "AQAAAAIAAYagAAAAEF14LLYY7eCCSH2cn4KBUq7mG4q9NJDGojm0Wx6A9lMC149dIo7VKWmhAWkr3oyYPA==", "4b15903d-0125-4890-a9a1-0de48d95dc79" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b80c0e5c-df20-4557-a44a-f92a481ca969", "AQAAAAIAAYagAAAAEKY7rJ1jAViVCto/mb+Gaw7l6IOnjC6Dk5nV99C3qXni+X+pAB8lOkveX55tkdeNOg==", "39e354b9-f563-4938-942f-a5b6bc865524" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd32d3b4-fcf6-4af4-acd6-f2f2c783d800", "AQAAAAIAAYagAAAAELfK8o4PALkb6Xffi1cZ+XRUH7EaDvOljLoNYSaBPa9TBdZ8Jx1FRrEuBUvjMVDe2Q==", "3ed0cbf5-156b-4053-851a-4076b28742a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0879f9b9-fce5-44de-af61-c053013f2c1e", "AQAAAAIAAYagAAAAEARpPYYUdCiUZ0OjBhl6itVZ/OcKrgH0qjwBC4eJCu3tXdbGOJ+OzJl+nVPIPd0EOw==", "9b5cb9ad-f7a6-452b-adc1-cf9cd7118107" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1a15cf7-d841-454d-931a-a77d66d34af6", "AQAAAAIAAYagAAAAEF6VNbqAvEgtCGrg7Z3585kvUX8NMBKneCFKTvMavG/JTGALXKbGA1nyg+Ee+kNiXA==", "9fd0b04c-181d-4a7b-b99f-787c3b029be2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a690316e-e44b-4316-844d-2e69e07c3474", "AQAAAAIAAYagAAAAEONHGP12NNteriwHYxv5Hw95k//uiM3RL+jyBkINjSLYphDXm7rH2e7E9URTLersxQ==", "029bc75f-72fe-4327-a7d5-57b82019256d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7c05532-9300-4541-889c-5da855ef1afc", "AQAAAAIAAYagAAAAEHStUCeq5yWFaFfGhITrIu+ciLGwxUAcTiaHXxX2G7QkeOU03+4tGjaXXlo9dgKtFA==", "cf665860-f156-444f-840b-ae6e046049d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75e0e7ca-2973-407e-af4d-0045d7e56314", "AQAAAAIAAYagAAAAEGMKjZO8bFnbgP5sng2Xt2Lf5AsgOqgGA+QbBqR+x6mqA4yrCjxPQGHW2oABEHX51A==", "f348363e-9fdc-4ed9-a5ed-33cb3bb53873" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff3b8dc8-c144-451d-8767-2e4b9028c9f4", "AQAAAAIAAYagAAAAEM9lpftN/Jx/ztr2ZtLzfkfcNbJTLOoGm10fchZehln3LjBzcHaoFL59hvEQW4sDHg==", "f146d60e-8d8d-484b-9af0-56e0f4f180f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bcc880b3-c84e-4610-8adc-06da18629774", "AQAAAAIAAYagAAAAEMj3LYvhbAPMCRb0CcGGfUp0dZpZ8REbFdoMsbZByefIjOJdl61hDHLvbf5sYw2PRg==", "cd6be89f-f64b-4c50-9011-fd9d21a77a71" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e59b2d8e-5d33-4e25-ab14-04888889ec82", "AQAAAAIAAYagAAAAEFrLDaij7hGqkTQf0SdCjMfL/8bMxG5yTDTx/hhi4uX3VMHHOlVvWufnPhDqZwd45g==", "f6ec071f-0c4e-4f3e-a52f-3d97965c0d33" });
        }
    }
}
