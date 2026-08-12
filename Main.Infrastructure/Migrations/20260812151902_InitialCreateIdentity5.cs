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
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a3e473f-2b01-4bdf-9106-81282305045e", "AQAAAAIAAYagAAAAEA5nwgEBCW+hvNRelt7RRyUlk3JAHBpmBGefwljpMWxPN9685zfhGdipiIqOj2mlkg==", "2153284c-4d49-45af-ba78-53e8c2cc562a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "457a22f2-976c-4def-bebf-55104cb4b9ae", "AQAAAAIAAYagAAAAEKrhy49E7Jlv3kZSzFJA31FYDBF+zCBHpHoyL6VoI2KTkBPtq4kMRD9xwB+xPdqxQA==", "77641c2a-847a-42d5-8465-ac8f4f5707c5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a58993fd-64ab-4f62-8e25-5b56da06fdd6", "AQAAAAIAAYagAAAAEK/e5mDjCdhYzxT1xJSttpIP9zb50WbA8VERk3/g7jkT5QJYUv4r6yNipeKpVMXeNA==", "79be1eb4-89e7-458b-93ae-049a22ea66b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df27fabe-ed5e-435a-aa61-3e3645c3c52d", "AQAAAAIAAYagAAAAEK99deENO1pxJyHoy1BmRNhPGIDPVyJf4sosdBA5la4735g5DyEz7leyIvcWo9v/3g==", "e21df732-7934-4712-9b7f-6b17b1cfb87b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "858948bf-966a-4220-9a8f-dbf32c72db7c", "AQAAAAIAAYagAAAAEL2gLwGBMVUsSr8AAwd+yXmGqw7FDtHhRTryEWwLGx5/TK91Mpr+i3T1JCmtQ3rpWA==", "d7d55676-1652-4e8b-a27a-f03b6dc1e018" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c7a6067-aba7-4e26-b141-929d8aa152ab", "AQAAAAIAAYagAAAAEA83vbiEQ3uMgN+02j8i0AkxzVAwGLejPjEh2O3vas1x1owDc+uT3c66uf33aBFYtA==", "dd6088d5-691a-4c51-8d0a-38106dfec605" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f32cd1c-69d4-4729-b663-106dc2ffaf7d", "AQAAAAIAAYagAAAAECk1CvLidyBG4xQFJE/PxPI5znuQgQDZW3yXaStaC1HX0n1UMc/JE54LLvotdJu2Eg==", "310cdcf2-8f8a-4fa4-a733-f83beaa8d26d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7692c3a9-9658-4de1-807e-86bb0320aa37", "AQAAAAIAAYagAAAAEADFqPLP8N6HZdn2Exe/QXL9K1jXOyvYsX3gtqwyqVzXyjOQR9QwqcGM/T73DtgjvQ==", "db4354f3-641c-42c0-9aea-14868e211be2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a53eef8-37b1-42d0-aa27-d74ed3a8e9b3", "AQAAAAIAAYagAAAAEBEpJNNPvM+I5CrKTw0E3M7gPinEWMwN8hv/TJztnW+2QYC7GE/eOz9IhpYfHBg7KA==", "094c1bc6-fb11-4580-b442-696f1480b418" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56d0feb3-9262-485d-bb78-3a2479e95bf5", "AQAAAAIAAYagAAAAEEFoKtlfiTyv1PnW5KBvzScVUO9bmSySlCii8MtGTxvi5SUUI0Kuj1keUqkMhN7qVw==", "2d34099e-f142-4079-8a85-32bae1215fe3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4ccedad-d4e7-4239-a0f6-01b9551963a6", "AQAAAAIAAYagAAAAEGnU8oo9ILgxCvRAArg+zzQAhzFhCGgesuCTMb1j+jq2BAqLH5aSpm6N8MsEpY4Ijw==", "ff0bbdf2-d017-44b2-a797-f300c80af676" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbcff812-37e9-4f65-be8b-a89e2d5d4a96", "AQAAAAIAAYagAAAAEINKa8K3S/vtWCn1dBkwYkCeckBcu4QRqf6U3yKVvGfq1BE6Bb8wFfeYcRvhc+JbUQ==", "34965b6e-5e96-42c6-a0d8-e1a83d33c7be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60a35e77-8696-43d7-8e7e-932649744697", "AQAAAAIAAYagAAAAECTKY18u5/clnRi1gQ38ZJxZmrSdG2X6cNA2rmi6E9sRONZwU0TdAT12QbRpZNdLcg==", "fa0d9bf7-f077-4fca-b7f1-7912af637a07" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed2ed7b9-9a16-4f8c-a58b-740d02a16aa6", "AQAAAAIAAYagAAAAEK/nHs3q96/6Uc3O0Vmr0MBwZ5IBlMP6oFm8Hs7q49E3c7t9toMhRdae8Qdwa5yMiw==", "7254e249-e000-4959-88c9-41277b4a6c5b" });
        }
    }
}
