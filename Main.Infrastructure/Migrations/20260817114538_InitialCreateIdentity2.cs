using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity2 : Migration
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
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HostType = table.Column<int>(type: "int", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "ApplicationUserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
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
                name: "TenantInvitations",
                columns: table => new
                {
                    InviteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantInvitations", x => x.InviteId);
                    table.ForeignKey(
                        name: "FK_TenantInvitations_Tenants_TenantId",
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
                name: "TenantThemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ButtonBGBorderColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyBackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuBackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuItemHoverBGColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuItemHoverColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FontStack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_TenantThemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantThemes_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantUserRoles",
                columns: table => new
                {
                    TenantUserRoleId = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_TenantUserRoles", x => x.TenantUserRoleId);
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
                    { "00000003-0000-0000-0000-000000000000", null, "GlobalAdmin", "GLOBALADMIN" },
                    { "00000004-0000-0000-0000-000000000000", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00000005-0000-0000-0000-000000000000", 0, "e0c0cbe7-f9fe-4eeb-9ff8-4ed5ef1218bf", "admin@system.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEAq+bEjMYXJrioukSvU1MjSz/MWkqY76jXoJs1U/edx/DgOq6KM/wD58OwOyQ9ShMQ==", null, false, "6f1bb1bc-65ae-422b-b54a-436c25d717c3", false, "admin@system.com" },
                    { "00000006-0000-0000-0000-000000000000", 0, "8a20ff09-bb11-4bdc-893f-a72bcc2df317", "tenant1.admin@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAENJTngM0d3rp6iWVJscnZOuxIyaMIq3RVbnoOE6q/vpBab7E/yTbAIgtdcYvge/S9g==", null, false, "8c7eb82b-b6cc-4ae2-9c0d-294b3a3f9c8f", false, "tenant1.admin@test.com" },
                    { "00000007-0000-0000-0000-000000000000", 0, "1f9880db-45b7-433f-8ab8-f44edbf5eef5", "tenant1.manager@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEDqQ/X01xRmZvcAd9hr4B7/WcdvECY0awM4+2U1kz3RqI65Ce9w4v0U3I0P68s6cFg==", null, false, "1f354df0-1a79-41c3-bf25-d776fbfe5f57", false, "tenant1.manager@test.com" },
                    { "00000008-0000-0000-0000-000000000000", 0, "383adc01-eef2-4653-9855-47fc4dad22a4", "tenant1.member@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEAmpjIMPyV9KSQzZjF+sGKcqMSKw2NjvDdqCMoLvEFBaIlKcWSSN8eWJloz6UT5vXw==", null, false, "03683ff1-0b28-4870-b824-5abb0910b04a", false, "tenant1.member@test.com" },
                    { "00000009-0000-0000-0000-000000000000", 0, "740d924a-7199-4ec6-9bb0-ec6b076a5862", "tenant2.admin@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEPGtaJTnVNRIcPAo2Vv8Vxei+n4JZ31rACdri/nrudqlgtrc5+u+cXwf0exQOAD9jA==", null, false, "976770bc-1a3b-4f54-9517-3ad0d269549c", false, "tenant2.admin@test.com" },
                    { "0000000a-0000-0000-0000-000000000000", 0, "6ed76de2-95b2-418b-a594-b383940f6f11", "tenant2.manager@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEM6jtpEqacVACVC9HRcuiyj2rMOTd4u29eJmFH2uAVhCgFVQllVeEzmK9uGuhd+VYA==", null, false, "bf1ef5d8-9e39-4044-9279-394fcfde0860", false, "tenant2.manager@test.com" },
                    { "0000000b-0000-0000-0000-000000000000", 0, "c8c037a6-0316-4c5c-8831-0e7eaa825a72", "tenant2.member@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEE24JtNRYN/vcPN/EmL1mR3M0SfV/hmKdXYi32olinCVchEj/TembrAppdp0HLIXmw==", null, false, "7850b377-4923-4306-be02-3a08203bcdcf", false, "tenant2.member@test.com" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Host", "HostType", "IsActive", "ModifiedBy", "ModifiedDate", "SecretKey", "TenantContinent", "TenantCountry", "TenantName" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), null, null, null, null, "tenant1", 0, true, null, null, null, null, null, "Tenant 1" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), null, null, null, null, "tenant2", 0, true, null, null, null, null, null, "Tenant 2" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "00000003-0000-0000-0000-000000000000", "00000005-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "00000006-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "00000007-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "00000008-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "00000009-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "0000000a-0000-0000-0000-000000000000" },
                    { "00000004-0000-0000-0000-000000000000", "0000000b-0000-0000-0000-000000000000" }
                });

            migrationBuilder.InsertData(
                table: "TenantUserRoles",
                columns: new[] { "TenantUserRoleId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "TenantContinent", "TenantCountry", "TenantId", "TenantRole", "UserId" },
                values: new object[,]
                {
                    { 2, null, null, null, null, true, null, null, null, 1, new Guid("00000001-0000-0000-0000-000000000000"), "Admin", "00000006-0000-0000-0000-000000000000" },
                    { 3, null, null, null, null, true, null, null, null, 1, new Guid("00000001-0000-0000-0000-000000000000"), "Manager", "00000007-0000-0000-0000-000000000000" },
                    { 4, null, null, null, null, true, null, null, null, 1, new Guid("00000001-0000-0000-0000-000000000000"), "Member", "00000008-0000-0000-0000-000000000000" },
                    { 5, null, null, null, null, true, null, null, null, 1, new Guid("00000002-0000-0000-0000-000000000000"), "Admin", "00000009-0000-0000-0000-000000000000" },
                    { 6, null, null, null, null, true, null, null, null, 1, new Guid("00000002-0000-0000-0000-000000000000"), "Manager", "0000000a-0000-0000-0000-000000000000" },
                    { 7, null, null, null, null, true, null, null, null, 1, new Guid("00000002-0000-0000-0000-000000000000"), "Member", "0000000b-0000-0000-0000-000000000000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRefreshTokens_TenantId",
                table: "ApplicationUserRefreshTokens",
                column: "TenantId");

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
                name: "IX_TenantInvitations_TenantId",
                table: "TenantInvitations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSmtpServers_TenantId",
                table: "TenantSmtpServers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantThemes_TenantId",
                table: "TenantThemes",
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
                name: "TenantThemes");

            migrationBuilder.DropTable(
                name: "TenantUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
