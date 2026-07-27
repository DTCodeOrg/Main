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
                values: new object[] { "974dbeae-cf5c-4224-9bb8-c700cd443664", "AQAAAAIAAYagAAAAEE/0aVy9Np8Jt8YBgg5jiy7whVKC6jbTmCFkKxX0SG7SqqsLQDBabyuI3uT3OTb8kw==", "850dda37-8f33-497a-bad4-c4155dd9e43f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc86d200-3d69-4a20-8210-5f9a9c5903b0", "AQAAAAIAAYagAAAAEOJDY36tjSJcbKLOFmcnD5mqFSk/GsyNWTE0gHbnMjX7jiJDTZGyBO8VH1bYN+Y4cA==", "af7b7910-4a6c-4438-9bb5-13d642d1d3b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e4818f0-bfe8-4565-b05e-3f0987403bc5", "AQAAAAIAAYagAAAAEAfb0aow56OBj5Bs8wXnAYb3BsE55nKP1VIMEm37Hg3CJjEz1R1MvT8kb4QdbqQuyw==", "e32360da-c787-4350-ad8e-1ccc0d32e870" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "219c131c-b0cb-4a29-9a55-71715f6b6c1b", "AQAAAAIAAYagAAAAEF79CA52smGv7ZK8enkjqLoeRpgThVCc8w1InU4z5mfachSHRVTpi5XaafneNUyAQA==", "41811e4a-ca97-4130-91bb-80c782acfa4d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc841822-b6b7-44a7-9d43-9f221a5fe472", "AQAAAAIAAYagAAAAEG6YVCRO59bUoRre8H39RAkGEFyamZUW/FFdRqMd9ve9pOL5RJuHI8409UNJYtR1cw==", "f3062afc-5bff-4861-b2b3-cfbc0f9eed5a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a247f7a-7f7c-4e7f-acc9-ea367958d96b", "AQAAAAIAAYagAAAAENhXHOG+FPQzVlERTztHlLMbMwIGuUIvbSZhOt1QDYbIiUD0rLQE4lyxP38+tTj15A==", "608d19e0-77fa-4269-8abe-f15abc57f430" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f12dab8a-327b-4ece-ad95-d8f30e407b6e", "AQAAAAIAAYagAAAAEJp/qpvHZHf6q1iFow7MuBjigKNNyqk2zCQ8RhzxHhAI/POK0g20UDTKbKa6rfuHCQ==", "c0052624-8f9a-4539-8224-49e06f72a449" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "417eca7d-2c41-4b46-957f-ed14868ea9c1", "AQAAAAIAAYagAAAAEDMcMeWlDk5uyLRvqaNwhvPN5N4jYh0YWK4KgIq/QHHLbud7LyqF5fuGqO73NtRC4w==", "8882a8fe-a78e-483f-bc61-43e16147dddf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6beb66a5-2adc-47ea-9048-3c0ab6f23a91", "AQAAAAIAAYagAAAAEE3it847wYlIuxrk39svRHskQLhHYO/+g/sU83gijKQfjQXGvEqcm4wy5gzFiR3NXw==", "5d7da80f-6772-4105-8df8-bf998052f5f7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83011f8c-3242-453f-b055-a2796592c816", "AQAAAAIAAYagAAAAEA1cPVZ+3iFDpS3NaNEpN/V7fUb4K5p1LuZRV9D0x0mXH473uEwUnK9XE6uEiKHmnA==", "88187e6d-b6cd-4e3d-b40b-47713c64e10b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a839861c-3ef7-484e-a1e8-7348fe6e0c19", "AQAAAAIAAYagAAAAEAUqfjq8M54jOx75s8scB3btkKCixj6ZnCb5k1btNDkRuKmK7Z56Y2C0hwb1rOm/MQ==", "2226be74-50b9-418c-af27-b55933a39c24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6418a5c-f344-4edb-becd-808080e92dd0", "AQAAAAIAAYagAAAAECdlj4fZcFILU/hTip6qgNiXNcUCRDSecrqdi6MF4Kjv2abyyeP2hN+nWh/rOR7fXQ==", "3afae34e-b02d-47f3-8692-2589964d05a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92d17598-4b75-4793-aa8e-aef0afb1a779", "AQAAAAIAAYagAAAAEIkYKocWGXQqBoaNpC+V5YxCc00L3+468LR5lOunhukFS35OQkK3G0tXrAFGCy4Hug==", "8eec66fb-9ee6-49cd-9714-d287308f7229" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5034e2db-787b-4bc2-a826-45a86dca4f1d", "AQAAAAIAAYagAAAAEGpR8ac6Y6bfhm+35PCRHsrfhBgAaS6AdpeCj5N94CgUdq3z6jYRv+xfM5lk2asgxQ==", "09e32fc2-9230-4018-ae56-c98723b01c6e" });
        }
    }
}
