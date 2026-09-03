using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7e235a3-ad67-4231-8993-071048db5b58", "AQAAAAIAAYagAAAAELaSZd75HLJFtBXs61OjlHn5t2QX+gcLEXgZB/UmtUf/qRktvLCco3vlMne7T8WgMA==", "2966a7ec-c5fe-48bb-8394-52a7dc025080" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7b2ca18-78c9-4807-b2fe-968738cb905d", "AQAAAAIAAYagAAAAEPxRs+snHUvc+42ZES9H9xlMv9zXy3UmPsVHGdxVkUAb0KuvQ4JbTReB+5LG0hhhzA==", "2348d733-1bc0-440f-a9f9-5bc59889054c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "633d639a-7c85-4e1f-b345-702653b6ac22", "AQAAAAIAAYagAAAAELDG04pqI7zhzjzTkwNGbBqAkHPyUqxgm2ZdNRziKfX619D7piF9NziaNMexv4H0rQ==", "c72a9806-b3ca-4869-acc4-6ba1fe1234df" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "795f250d-2b16-442f-a76f-f4fdadb68686", "AQAAAAIAAYagAAAAEKYzuOJvPj6N3jcTuxR+WkZcKToiFdgpR8bHEUCJsGi9ZmLY0zfi79rY1ihCN0Z2gQ==", "e67f4b85-c0a2-4381-89a3-cf9c2381eab4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5241fe55-3930-4f5b-acb3-2ec9c7276de2", "AQAAAAIAAYagAAAAELf9B5bohu33VlvouBi/DjW90PxLNffjiWhQfjlGR8ly/Z5MVyIQ1buQ/hpvMHLdww==", "2943f3f8-ade2-4b17-8029-54e2555d525e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "efa4704f-afb3-4627-ac20-4194a4209e16", "AQAAAAIAAYagAAAAEOxGggJ9wWosKHVVEoELiMrA96YqCOmNfBm6hPuReOfjf0M5r58J+QQxjOVfYXksYg==", "2fb86047-e1aa-4b83-a90b-fb20f32d1c87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a25021ca-79f8-432e-a440-775160d9bae1", "AQAAAAIAAYagAAAAEOOi1J5t604de513V7zDVs07BmkN6brGvWw9k5KbSjDnO55bzwiX+d0b1wF91Z6S8g==", "89f2d250-3d74-4d9c-bd7f-ca1caf172d77" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9038892e-bb93-45f5-b687-7dc944ab19fa", "AQAAAAIAAYagAAAAEBzAAkV+XTQyrUdn2lBq25B5JglK0qqHwVXhtEvvzxSPI17CcwSvskNYwyTzCCfogA==", "603f4e87-1413-4c45-aa19-2b034628b57e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fee6fd6c-89a4-490d-8328-dd04476583c8", "AQAAAAIAAYagAAAAEP58OGxCJkXrMoZj/bFUmCRBeJrneZNU2rt3XxkU3PtmTJMnM+Kv4v0HYAyXhO/DOg==", "500bef51-0171-4cda-ba4a-4e4fdaf753fa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
