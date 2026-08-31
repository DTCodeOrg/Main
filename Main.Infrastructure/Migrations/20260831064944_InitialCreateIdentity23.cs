using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f40fea2-daa4-4a55-a269-cec7a7d4f053", "AQAAAAIAAYagAAAAEOVbZ+HaJLvMaPJ8sJXOMqxXcBAXnF03FlUzG4DrKgn10OQASYJatRTGI1Zw1xEftg==", "c425184d-049f-4a8c-9c59-a133e008c1aa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b2bea56-c315-4d77-a926-93c01b57694f", "AQAAAAIAAYagAAAAEFB72Rgyob/yltKyir9OF3YJHE1G3PLJsbq7qPAtS0A/IcvOlYGo9h/R2JG4YwHyXQ==", "2ff56c5c-a243-4b45-9a35-883919082311" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04218617-4d25-4025-9532-15e3248a8c38", "AQAAAAIAAYagAAAAEPcFSsgrIrvD8HUaxya8xAOSMAiF17oKvu9eHGL8IYuFZ8NfF7Fo+F40fdvNmEtjZg==", "3b242e70-0599-4152-ac34-1463bf968bcc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1e32db8-427c-4e24-bf33-a8335cdeb797", "AQAAAAIAAYagAAAAEEdnGOvJqzDo5HQUaCOQpLusUjjODwPdqjP6rFHpvG+QKZdqaB4yOxxECWtDHDq8bg==", "d9f11e2f-5ae4-4104-86c3-9ab80c69523e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60512359-4fdf-48b4-9b8a-6f8ed28cc6aa", "AQAAAAIAAYagAAAAEI00AT8c17hXSKl88jxkK8+KfSaQwjMbW9jVZcjGGfeWxLYMt2upSzLwBQESFfx11Q==", "03e9f377-ba9d-4422-9442-ef6d8f4e361f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1288f31-0f0c-47e4-b127-dbc458a71ceb", "AQAAAAIAAYagAAAAEAV2Zagyj8YhvQnlTBJykRV/1cQSv13kDdSzfyGluII7OXnK3UWWVevfaVLkrXQ44A==", "0776df0b-3bbf-4bf9-b9be-4b087e7127d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "081ca6c3-8ddd-4fc9-bacc-782b30476fc4", "AQAAAAIAAYagAAAAEJsC7yJP90lJJkzeZd847tYJ+j42D4RwAHsdH4ZKyEHLav0kwPzDXwalOCTL3gqB0Q==", "6d1102cf-c7fa-47a3-b231-f9cd3c79905e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3647ec87-e685-4859-ae27-ea153ab18441", "AQAAAAIAAYagAAAAEIYOs8hopG5Tqx3tnClSJTrqxY8NwYe+5Kk+J5ttW9WngSTbmVUNVvLUO7q15VMpLA==", "3c0aedbe-6cc2-4be1-b729-95e34610dcf8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5970ca7f-8b3b-42b5-a7bd-d753c85779f8", "AQAAAAIAAYagAAAAEFhIsFszXNRPo46ktf1gdrqhoJkRMzqa+oFBfJ/uRpp2PMtGwuPGv3ANkum205kTdQ==", "3483be7d-0938-4e1d-a5f8-0a85e445e3f5" });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000011-0000-0000-0000-000000000000"),
                column: "Host",
                value: "finearts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21d4283f-bf32-48d8-a244-364ada042e05", "AQAAAAIAAYagAAAAEGt6TFQV0uZi7iiWMKSN9Ez+S4Z7D8DBonnKeJwu2n+WOsAKiCOT1lnhPoYFit9lsA==", "0d32b4ff-3854-4eeb-984c-3afa5513ee81" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37392816-2b40-462c-832b-bfcc0e1ede3f", "AQAAAAIAAYagAAAAEFW2ldkGpfI/Kdk0ba6fFdWCL64NCNA4TyH4oM1C57UFdZFkyvR5hFi4FYqW4XxAgA==", "d5ebf779-39ba-40c5-97fd-972544140174" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fab21e4f-50c7-4617-bf68-55fa6075beb8", "AQAAAAIAAYagAAAAEJ/Zo38Yw4Ujgqm4BiOl4aRRM6nh0mpZNkfkZPgY2D338OHtNMCwkRC8/2rqfM69LQ==", "efad81c1-1204-4cd9-909a-e355f71f854a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9e66ae4-c503-4f4d-86e8-bcfe04c33af7", "AQAAAAIAAYagAAAAEHRxbl2CAZXBkF+F4t5yjIGTqafDFA2Sook4U45kHcnNGz9pUOeWR1HL/NGRZTSICQ==", "84ac87b0-dac5-4d0f-b8db-7aa24f59eea6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4622912e-4bda-4c5a-b3da-8dc3cabc495e", "AQAAAAIAAYagAAAAEFJrsxE6cXYQAUSfksHjKnnm6YxpKuI0sJgvfm/ftk/amg+eg6EnhvDsJv+gtIKBTw==", "782ebdaa-916c-4da3-888f-73011122c8e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b975e7df-0933-4a28-b754-c966caf1788a", "AQAAAAIAAYagAAAAELwaErHt5+WONW4QK7DgCN4i4UlVkihqJ4d+YPM8QwzmiP61YGaf5t6CM1HbURDebg==", "54ae93dd-01df-4e78-b7bf-d63392ae579f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d571ad77-6194-453f-9533-9e5aa8756384", "AQAAAAIAAYagAAAAEPiAWFhIXDXvlVDRpmIn5QWuDx+X9KBAgeK6lsEqmWxZhRZ3BdbctQpda8vNCv5hQQ==", "dd5d5920-6fc2-4c1d-a9d8-954778fc567d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9257295f-9a97-4b9f-9d5b-e5a084912cbf", "AQAAAAIAAYagAAAAEItMAgl3zjruDdA94SvQqt1IIQVbhGOGeuscdmxTF/EQj/aboYYxxm8BuN6g7qm0PA==", "90b2f505-9837-43d3-8f6d-2ec991288eb5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2f0b468-2979-4bc1-ab13-c9f5e5cccfa9", "AQAAAAIAAYagAAAAEDNCF6Sw9zmoCRLT/z5lPXmmjr93b32XySOC1pUPbsMweEFbThrLE2DPjWmcIBIwJA==", "2d0e228e-9129-4062-a200-3f18146a82d2" });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000011-0000-0000-0000-000000000000"),
                column: "Host",
                value: "fiearts");
        }
    }
}
