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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a647a6a-41cd-4631-96e0-ac8caca9a3b4", "AQAAAAIAAYagAAAAEPe0AxjSD5m+CwWKTAmWeBEaAxfiHLZ3qabsGGeJfLKqkSMRJF48Tjqj1ERiz7/VjQ==", "456bc34d-94a5-458f-a3cf-ce012357b19b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "205b23bc-f10a-4076-95c5-e36a1f9766ed", "AQAAAAIAAYagAAAAEJD26vrTt7gdtKGW8SRhAkQuhc8MMXpWoLCmW8KzeC0AGm2Y6Gj9F2q9O8PSIoGJyw==", "18f7c858-6cf8-4ca3-9131-6f9a72d2778a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c238508b-9db7-4f45-a901-c770b0b3c4d7", "AQAAAAIAAYagAAAAELSn4LzVTt3oAmXHbzvew3sAnygjllAGaGGPMzQML2K4GXfKtTbf3ky5Ic31cO5LUw==", "ce8b49c9-2be4-4eec-98be-f339caf20683" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d0ef75e-31e2-488e-b7a9-f0e0671beabd", "AQAAAAIAAYagAAAAENj2A53JYqQIJKIk4GZOaWeuqZUwjpyVaiUBJlPtqTzujdaKXMPGhW2Bswp993Qt0Q==", "7ac06105-c461-4593-84e1-ba1d5bc16e26" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ed50515-269d-44b7-ac89-bd0b2e6add9d", "AQAAAAIAAYagAAAAEHfGt/xCZjgd64v8mZnwjn12zvxqxEZxC8p9csW9TQ0ezBNycroDZSBqp+1RYUELpA==", "1385894c-de9f-4a96-a644-f64e43b2452d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "abadc0c7-d95b-4bb1-a0e5-e0582eb1c1d6", "AQAAAAIAAYagAAAAEArKIUvaYWAPNiymQG/OrY8QO2202Usr93OgovhNQPqNga/Oj3f8HxHa1ua2iZDzlw==", "c1c27862-127e-4c79-9568-8a7b08e0f046" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a20f8f6d-2ae8-4d59-a504-f8c201e128d3", "AQAAAAIAAYagAAAAEF7zZbAwVddUPJA5KyIy01g53t/RxvqHRSYq6TflDdFsO54sGJmzn4CJPH+J3IC2sg==", "7a2c4869-abe8-46f1-9bd3-5c3d33cb3573" });
        }
    }
}
