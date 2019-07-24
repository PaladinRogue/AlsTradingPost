using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authtentication.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "apps");

            migrationBuilder.CreateTable(
                name: "Applications",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    SystemName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthenticationServices",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    ClientId = table.Column<string>(maxLength: 1024, nullable: true),
                    ClientSecret = table.Column<string>(maxLength: 1024, nullable: true),
                    ClientGrantAccessTokenUrl = table.Column<string>(nullable: true),
                    GrantAccessTokenUrl = table.Column<string>(nullable: true),
                    ValidateAccessTokenUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identities",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    EmailAddressHash = table.Column<string>(maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthenticationIdentities",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdentityId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Identifier = table.Column<string>(maxLength: 255, nullable: true),
                    PasswordHash_Salt = table.Column<string>(nullable: true),
                    PasswordHash_Hash = table.Column<string>(nullable: true),
                    AuthenticationGrantTypePasswordId = table.Column<Guid>(nullable: true),
                    RefreshToken = table.Column<string>(maxLength: 1024, nullable: true),
                    AuthenticationGrantTypeRefreshTokenId = table.Column<Guid>(nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 1024, nullable: true),
                    Token = table.Column<string>(maxLength: 1024, nullable: true),
                    TwoFactorAuthenticationType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationIdentities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthenticationIdentities_Identities_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "apps",
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthenticationIdentities_AuthenticationServices_AuthenticationGrantTypePasswordId",
                        column: x => x.AuthenticationGrantTypePasswordId,
                        principalSchema: "apps",
                        principalTable: "AuthenticationServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthenticationIdentities_AuthenticationServices_AuthenticationGrantTypeRefreshTokenId",
                        column: x => x.AuthenticationGrantTypeRefreshTokenId,
                        principalSchema: "apps",
                        principalTable: "AuthenticationServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsRevoked = table.Column<bool>(nullable: false),
                    IdentityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Identities_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "apps",
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    IdentityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Identities_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "apps",
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypeChannels",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChannelType = table.Column<int>(nullable: false),
                    NotificationTypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypeChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTypeChannels_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalSchema: "apps",
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationChannelTemplates",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NotificationTypeChannelId = table.Column<Guid>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    Template = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationChannelTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationChannelTemplates_NotificationTypeChannels_NotificationTypeChannelId",
                        column: x => x.NotificationTypeChannelId,
                        principalSchema: "apps",
                        principalTable: "NotificationTypeChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationIdentities_IdentityId",
                schema: "apps",
                table: "AuthenticationIdentities",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationIdentities_AuthenticationGrantTypePasswordId",
                schema: "apps",
                table: "AuthenticationIdentities",
                column: "AuthenticationGrantTypePasswordId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationIdentities_AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "AuthenticationIdentities",
                column: "AuthenticationGrantTypeRefreshTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationChannelTemplates_NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates",
                column: "NotificationTypeChannelId",
                unique: true,
                filter: "[NotificationTypeChannelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTypeChannels_NotificationTypeId",
                schema: "apps",
                table: "NotificationTypeChannels",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_IdentityId",
                schema: "apps",
                table: "Sessions",
                column: "IdentityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityId",
                schema: "apps",
                table: "Users",
                column: "IdentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "AuthenticationIdentities",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "NotificationChannelTemplates",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "Sessions",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "AuthenticationServices",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "NotificationTypeChannels",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "Identities",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "NotificationTypes",
                schema: "apps");
        }
    }
}
