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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "680bf684-25a7-4d06-bc50-d9bd20480977", "AQAAAAIAAYagAAAAEA5SCRgVBlBv9iRZ3JzaTHq6VpW5vehmmou50jGG8dRq1hcYnd161txORHlVO1GufA==", "f32ec35a-43df-40c3-9a39-9f116a0e494e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f8ce95c-fc34-4eb2-9495-5c591fd4722c", "AQAAAAIAAYagAAAAEFTOF3b8uH0EdfeApGAx7cpF+cb+KwdUgGyuk23yemZ8ziJUELiHg/n0P9c7p1hrGg==", "e0af07c5-5f44-46dd-80e7-76f6561cc372" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a84bbcf-b712-4b53-bfc3-46d5a7755a38", "AQAAAAIAAYagAAAAEF2T+tzLzdtoXVvxqM5Pgb+tnpChTYaeN0fpneCoToV2FGW2XwyfLaO9GE7HqVJJEQ==", "02cdbdb5-08cd-4526-94ed-d1f93d201ed0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "452165cd-d599-48ca-bfd7-39a90ef0d13a", "AQAAAAIAAYagAAAAEDkQvpw6FKAChXndVxbpsmF4SdfsBXzH9QqRg6K8cZUoP7W1y7rG8IeEvPGKPTGuUw==", "17425112-b5c5-4e2c-806d-4f5f7ac98559" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1817f4f7-db84-4ff6-85a2-896abbd056ce", "AQAAAAIAAYagAAAAEEgbFYQckliYR0OPvkdse+5PnJStPPtDvL47X721U27iXdl6MAL9gAlxx8x/Ab2jUQ==", "592ae9d7-7f66-403e-a0ce-bd008868e6ea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad3e8a18-6d25-4bd0-87c4-0cd8d63061aa", "AQAAAAIAAYagAAAAEK8wM8hmFeRvIizMtZzquQmaTaNNZK4Bxaa2MHfweU0lqVrbnLe4hCjc8EIzrMoJEA==", "e57bb272-22d9-40bd-92cc-6b7ea513d3e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9740d5dc-ec58-487b-869d-ae9e2bbbc344", "AQAAAAIAAYagAAAAEKBgRNZU+XDKx/wL++hX7lvkP59Ybj+fTxjeh/hSZxMTgaud+lNOSG+Pj4bWqbyuXw==", "5a8785af-7771-4398-95e8-6d86e06ddba0" });
        }
    }
}
