using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b3d2c10-71a5-4eec-9cb6-1f5d1ddcb05d", "AQAAAAIAAYagAAAAELm/IV3ll4O2RcpuqDE4couMDwm8zCXwzjTW7ySn148heP8Ug9eBzjhW22KVoLHs/A==", "5c0846e3-333f-4d6b-a790-9f7679b48a3d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6067e88c-28e4-4266-b82f-ac53bfdc33c2", "AQAAAAIAAYagAAAAEBpY5/rHqoKF6w7mGEYC2C9hxPYgUWjxyKsrYdHQfpB9uv3brVU6fiZ6WZG5rctw2w==", "133537ea-6845-4cda-bbba-030de58b5c85" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02b0d8b9-d659-47b8-8320-f2b2eb867e04", "AQAAAAIAAYagAAAAEHZtR8Rwtg1AvkCA6RuR8PM96wTUDB+S4s3ZM4EtO0LnEqWJdRn/CtHriDuOa3QAWg==", "c4d5b412-95eb-4207-966e-0ee57c0e0ef0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4400e34-324f-4bdf-a1a2-a8d0ba2c8adb", "AQAAAAIAAYagAAAAEGaGmGgz9JQPpmjd7m/iuwwqqCXiBO5T/MrpX10NK7LPUCRCAoalhT3JHZUyQsJPcQ==", "2e234ab2-c47c-4bb2-92b5-fc7acd7a7bc3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f6df0bd-0ca8-4b3e-a0d6-fa91aa178b83", "AQAAAAIAAYagAAAAEAEqZaeF/y28/6DnhapxnsARvR0qgPqtCQ3oj3G4rgWxCTviZ1EoHVio/te/CujdPQ==", "42b4e327-03d6-4fdd-9aca-2775eb24f236" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee46d3b3-3c19-4a56-9cbf-520c768a64db", "AQAAAAIAAYagAAAAEDQZsLTqmYZr6fAykmqrMlt0RsSPyp+Wir9TBQMHzhaQqvRixWsZyjp5KKHt4wBU+A==", "45e05301-517a-41d8-a169-fc1ba17c9c99" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cbeb95c8-3a78-4582-8386-d19ad4cb4bbc", "AQAAAAIAAYagAAAAEC9UmGxZ3CxR8DKLWyCXlZm0xI+vX8ka5WuEDYhGZeQ4teZ8pJ5b40FwXWiJuIe3qA==", "6a874956-65b8-46b7-a3ff-9f1b90ac0ecb" });
        }
    }
}
