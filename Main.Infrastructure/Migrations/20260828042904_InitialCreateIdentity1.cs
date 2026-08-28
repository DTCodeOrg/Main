using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c651deaf-55fd-4180-a6ca-80a154b57c4c", "AQAAAAIAAYagAAAAELtZj4mnKOjYDyWc4DScBxI97m3PWelVIDLaJhJHWSpTlY82oU8iGrs4Yu1pPtSnww==", "9ec808b2-1fe0-49cd-8325-7f14d1f38a20" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7de4f6a-cd5c-49e9-88e8-d0be84260260", "AQAAAAIAAYagAAAAEPO/uirdrZqkwbcFbidbP0wXH8cFx0Sk5brCwweESR3S64dUA4FhdxRfqdd95DQrPQ==", "1ad289cb-8928-491e-bb89-a2a69138da79" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e848d85-5f5d-4887-9e54-6e80a2d4ce83", "AQAAAAIAAYagAAAAEDIftjWK6EoGvCLQxv7GhUc2bgnXn8mSmV2VeBCbO8sYdgIJwItWJpESqIpFc48yRg==", "663a7b86-fa79-450c-a961-84ac12ec93f3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20fe587b-160d-4bd6-b77e-c4997e054d76", "AQAAAAIAAYagAAAAEKpZv3PTzpqGaV22XOWdB1ZyYOqSn5KS5idt49ol7MS5Id3/i1PyaSzrROUHYXFjww==", "b2104df7-2f64-42de-a70d-e16b6c94581f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "102876b7-a286-44ff-9ab7-b402f90499e6", "AQAAAAIAAYagAAAAEMYSzy5pwWfW56OUSvka26c9R/yRKmQYoltJ7xReZumlPbVepiqDcT17OehrM/KVMA==", "8109755a-d1b4-4403-ab1b-43c0d3dc711f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eabbe2e9-a6ab-4541-bb8c-bc339796b575", "AQAAAAIAAYagAAAAEE+aG8Nloca4/uq4S/7UdIeSfG1e4NjdKgN/oj68DQ9Z5SG3Wldtrc1UDnR/nvNfJg==", "bd47d479-87ef-4395-81d9-78fbd7809f9e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7931f45-1a6f-434b-96e8-34584e40ff19", "AQAAAAIAAYagAAAAEFV7DhRzSSouSCqLcS1hmcd0Zu53KizV5ViAVG6izFQrLGk2KyaCremg7dFek5iuGw==", "e642887e-20f0-414a-b12a-610aa4a6ffc8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
