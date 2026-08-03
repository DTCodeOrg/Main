using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "950c48a5-3fe0-468e-8544-731b36557b33", "AQAAAAIAAYagAAAAENso1h6of65trNWVpS8A5nRrx/YHhE9UNNL9UeZQXV9yPe6TOHlB1ZMPCx38YHTUQQ==", "f8cb1ded-6356-459e-a438-b7ce3679e773" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb700e59-3e3d-4b68-a140-2f9cd2288bf0", "AQAAAAIAAYagAAAAEHjPZAeSZcgzeGnIlTzjmuj7LwvWbS5FOyZSxe0JwMz1mnB5+byvro/HFfdKcNgxZQ==", "a1a16fef-d845-4649-b4a4-47e02e1e3c9d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bdc71397-d129-4acf-9237-0f24770b259f", "AQAAAAIAAYagAAAAEDOGR/uYEI61XCue9ivtYsmIjy0jSGvSJP3Xnc9kIzsGehs8LsubFUlFH0Q2oe8OqQ==", "8ba8664f-af17-43d4-a898-0f3174283ea2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8189717f-2c61-4945-ac91-2824485487ee", "AQAAAAIAAYagAAAAEDGEjnbKEONZm45uc3I/1u3+40MiemRmZoQRuFUiVGJw1CjHGuEiIIWkPiCoe1h3tA==", "dc29a96e-2d24-4e04-b354-8832b56edb20" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0070cbc-efbb-4421-afd0-a539760ef571", "AQAAAAIAAYagAAAAEC+YkhzK+8OxDYoCJt3Bo3q9JWXdCYcun7tDMTDPbBLEd0GIB/YNy1cmvXgXxAdoEA==", "55670741-dc30-40a4-8149-d9469c53c105" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a175f4cd-71be-4a94-a183-b82d55068bc0", "AQAAAAIAAYagAAAAENFhZ+Op4cqQWt+GRNsARcRQfYdIJ5b47Cto5XUAHcoG+3SE+IxUR4PFSU/x6ehKVA==", "5327233c-b6b6-4c3a-a7be-7d6132db5f11" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "349a7846-55b1-44bb-a294-c753e2e76309", "AQAAAAIAAYagAAAAEBer+mBPUIHKgDC+Vraqd9+yq17clE8QVyjsn9eyr/uZ0RfbENJPhvHrZ55M5bT24g==", "ce2ede3d-2608-40f6-84a4-88e79518cd24" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c63aa70-9af5-44e0-aa6c-6ac627473b56", "AQAAAAIAAYagAAAAEAeaasTdNLWVTLh9lV1KpLqRHQmzLlD4qjORAtiMspMY7ibqquXw5Ore384jzuDzeQ==", "72d5cc85-9e55-42f1-a1cb-ab138cf79112" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24ced90b-8be2-42d2-aba2-ac71da754fce", "AQAAAAIAAYagAAAAELcmiY5zESFHAyA7mxlPBVY/BldqK3KUgNhWvV35KRTxeLJtZXClPZUXC690BL+Dmg==", "5cb07087-ca39-4670-9811-254504ab23b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21088b06-7c85-4ec7-bf76-c908033c12d2", "AQAAAAIAAYagAAAAEHqd9/Vxl472sEnLuWsBFMX26M3Oeaha09kq3KsDsuxH0dEI5a0aA0p92EO42kPwQw==", "a701399c-4d3d-4f37-98d1-01655689faab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "627450a8-6f1e-47d7-a117-eaa78645e668", "AQAAAAIAAYagAAAAEOYbAlIAWVM1uKQ8Xt5Ge0vX7oKWGoc38jKn6cp2rjWbQT+FYFiU2mNYhX9DgZGupQ==", "9fd935d2-7671-4b22-9abb-35c9ab65155e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46c386f8-e61e-4344-b4b3-1c00df836089", "AQAAAAIAAYagAAAAEA9zPykUbE6uLt2ru0ZhTDULwQ9u2Lr0vxvn26MoMvNEcrUEMDL73tQLo4iNIAxubA==", "0ec14528-f8b7-4b8a-8f31-909bf9131b30" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b160df0c-ff2e-495d-acfc-6ddc7af688e7", "AQAAAAIAAYagAAAAEIwXd1Ize0JhD51ZcnmjcD2tA0OEB1ANfddsbiRT0ddGm/Fon+SJd4sB8vh+XiXDog==", "f2c14a36-659e-4658-adf0-dbc9f50ccc7c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d63eecc-4cf8-43a4-b61f-42ed1b68f6bf", "AQAAAAIAAYagAAAAENmoQBGRNvCAhzWiK/eEZBjnlFDCMN2K4a8esAtfDcgLvuEXjjh1gutsWQcVcBfTAA==", "99fc0292-1f2c-4532-9a88-7324617300f5" });
        }
    }
}
