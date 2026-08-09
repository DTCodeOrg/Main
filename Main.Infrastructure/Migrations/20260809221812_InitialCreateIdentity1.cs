using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantInvitations",
                columns: table => new
                {
                    InviteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvitedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantInvitations", x => x.InviteId);
                });

            migrationBuilder.CreateTable(
                name: "TenantThemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontStack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoFileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantThemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HostType = table.Column<int>(type: "int", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantThemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantId);
                    table.ForeignKey(
                        name: "FK_Tenants_TenantThemes_TenantThemeId",
                        column: x => x.TenantThemeId,
                        principalTable: "TenantThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRefreshTokens_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailOutboxMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailOutboxMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailOutboxMessages_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantSmtpServers",
                columns: table => new
                {
                    SmtpServerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmtpHostServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Port = table.Column<int>(type: "int", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSmtpServers", x => x.SmtpServerId);
                    table.ForeignKey(
                        name: "FK_TenantSmtpServers_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantUserRoles",
                columns: table => new
                {
                    TenantUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUserRoles", x => x.TenantUserId);
                    table.ForeignKey(
                        name: "FK_TenantUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantUserRoles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "GlobalAdmin", null, "GlobalAdmin", "GLOBALADMIN" },
                    { "User", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00000002-0000-0000-0000-000000000000", 0, "5ae127d1-14dd-4006-8ecd-2b980cf15eb0", "admin@system.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEPFQL0/jAYKUD6PeOADU9cW7Jbz59F/iJKVe85hsB69cZW1foxQNUeYegAdNex+WQg==", null, false, "fa3b007a-9169-4897-b56e-4023598072b6", false, "admin@system.com" },
                    { "00000003-0000-0000-0000-000000000000", 0, "0aadd326-3c27-4fa7-8d67-60afdf8e1259", "tenant1.admin@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEFNi8s15shiE7nCHGp00XovGjBNxjTY6xZUtr06cFxEj8XG3LGwAqlNQqE4gJFHTgw==", null, false, "01b03237-419e-4185-966d-075a12438545", false, "tenant1.admin@test.com" },
                    { "00000004-0000-0000-0000-000000000000", 0, "f8f05cb5-a8f9-45aa-aaa5-eb45f186720c", "tenant1.content@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEHjZp04ND/vRypbUyQF+Lj0K0qS4ZZDV0ko8hWZDZuhQDv3UqMtrmbVEP8JYJrmXHQ==", null, false, "868ce34b-426f-467a-aa93-026ea97f8812", false, "tenant1.content@test.com" },
                    { "00000005-0000-0000-0000-000000000000", 0, "7731b98a-af9c-41eb-bbd7-667bdbf2ac63", "tenant1.member@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEEDZjltJFCEZ/lArqXwUSdiNdJRJIuNRtHM1CQFUvCS1f9ngPAH0JHcoWEUE83UMjA==", null, false, "a07785e7-07ef-46c7-999c-bce72089a9b5", false, "tenant1.member@test.com" },
                    { "00000006-0000-0000-0000-000000000000", 0, "10e6a896-6c17-4175-929a-0e7198d7da1f", "tenant2.admin@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEEAs90xJDEebgEoQszvjy+VQJRb1zvmSNZggrobfY3i2iA6lHqy256S7UJoArLv/yw==", null, false, "50aa7e48-dd5e-4caa-b853-2654befa7fd1", false, "tenant2.admin@test.com" },
                    { "00000007-0000-0000-0000-000000000000", 0, "06c0402a-7002-4a9b-a56c-73f312e99db9", "tenant2.content@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAENROfxEPfRZ/wYrEBLz3fknyR1xtsvEOTOo1KXjZJNrDGw6Xh3tgFMLey+wyik2i3g==", null, false, "254c279a-e332-4d90-a386-8f9383d4a5f8", false, "tenant2.content@test.com" },
                    { "00000008-0000-0000-0000-000000000000", 0, "aff083ee-b911-41c4-a4e9-4ac539384313", "tenant2.member@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEIs1De2xpGICnlKHwiv+LUEiODQwrh1IrC9hRyKi+uV5njcI+mFEZlWmGac2jHC06Q==", null, false, "cf22b57f-fa4e-4ccd-8c1a-bd7fa944f75f", false, "tenant2.member@test.com" }
                });

            migrationBuilder.InsertData(
                table: "TenantThemes",
                columns: new[] { "Id", "BackgroundColor", "FontStack", "LogoFileName", "PrimaryColor", "SecondaryColor" },
                values: new object[,]
                {
                    { new Guid("0000000e-0000-0000-0000-000000000000"), "#F7F8F5", "Garamond, Baskerville, 'Baskerville Old Face', 'Hoefler Text', Georgia, 'Times New Roman', serif", "", "#122A1E", "#879882" },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), "#F4F6F4", "system-ui, -apple-system, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif", "", "#1B3B2B", "#728C69" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "GlobalAdmin", "00000002-0000-0000-0000-000000000000" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Host", "HostType", "IsActive", "ModifiedBy", "ModifiedDate", "SecretKey", "TenantContinent", "TenantCountry", "TenantName", "TenantThemeId" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), null, null, null, null, "tenant1", 0, true, null, null, null, null, null, "Tenant 1", new Guid("0000000e-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), null, null, null, null, "tenant2", 0, true, null, null, null, null, null, "Tenant 2", new Guid("0000000f-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "TenantUserRoles",
                columns: new[] { "TenantUserId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "TenantContinent", "TenantCountry", "TenantId", "TenantRole", "UserId" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, null, null, null, 1, new Guid("00000001-0000-0000-0000-000000000000"), "Admin", "00000003-0000-0000-0000-000000000000" },
                    { 2, null, null, null, null, true, null, null, null, 1, new Guid("00000001-0000-0000-0000-000000000000"), "ContentManager", "00000004-0000-0000-0000-000000000000" },
                    { 3, null, null, null, null, true, null, null, null, 1, new Guid("00000001-0000-0000-0000-000000000000"), "Member", "00000005-0000-0000-0000-000000000000" },
                    { 4, null, null, null, null, true, null, null, null, 1, new Guid("00000002-0000-0000-0000-000000000000"), "Admin", "00000006-0000-0000-0000-000000000000" },
                    { 5, null, null, null, null, true, null, null, null, 1, new Guid("00000002-0000-0000-0000-000000000000"), "ContentManager", "00000007-0000-0000-0000-000000000000" },
                    { 6, null, null, null, null, true, null, null, null, 1, new Guid("00000002-0000-0000-0000-000000000000"), "Member", "00000008-0000-0000-0000-000000000000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRefreshTokens_TenantId_Token",
                table: "ApplicationUserRefreshTokens",
                columns: new[] { "TenantId", "Token" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRefreshTokens_UserId",
                table: "ApplicationUserRefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailOutboxMessages_TenantId",
                table: "EmailOutboxMessages",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantInvitations_MyTenantId",
                table: "TenantInvitations",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_TenantThemeId",
                table: "Tenants",
                column: "TenantThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSmtpServers_TenantId",
                table: "TenantSmtpServers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUserRoles_TenantId",
                table: "TenantUserRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUserRoles_UserId",
                table: "TenantUserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserRefreshTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmailOutboxMessages");

            migrationBuilder.DropTable(
                name: "TenantInvitations");

            migrationBuilder.DropTable(
                name: "TenantSmtpServers");

            migrationBuilder.DropTable(
                name: "TenantUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "TenantThemes");
        }
    }
}
