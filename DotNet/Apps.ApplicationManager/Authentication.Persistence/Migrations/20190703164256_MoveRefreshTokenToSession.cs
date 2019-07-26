using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication.Persistence.Migrations
{
    public partial class MoveRefreshTokenToSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthenticationIdentities_AuthenticationServices_AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropIndex(
                name: "IX_AuthenticationIdentities_AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropColumn(
                name: "AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropColumn(
                name: "TwoFactorAuthenticationIdentity_TokenExpiry",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropColumn(
                name: "RefreshTokenHash_Hash",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropColumn(
                name: "RefreshTokenHash_Salt",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TokenHash_Salt = table.Column<string>(maxLength: 255, nullable: false),
                    TokenHash_Hash = table.Column<string>(maxLength: 1024, nullable: false),
                    TokenExpiry = table.Column<DateTime>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    AuthenticationGrantTypeRefreshTokenId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AuthenticationServices_AuthenticationGrantTypeRefreshTokenId",
                        column: x => x.AuthenticationGrantTypeRefreshTokenId,
                        principalSchema: "apps",
                        principalTable: "AuthenticationServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalSchema: "apps",
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "RefreshTokens",
                column: "AuthenticationGrantTypeRefreshTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_SessionId",
                schema: "apps",
                table: "RefreshTokens",
                column: "SessionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "apps");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TwoFactorAuthenticationIdentity_TokenExpiry",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokenHash_Hash",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokenHash_Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationIdentities_AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "AuthenticationIdentities",
                column: "AuthenticationGrantTypeRefreshTokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthenticationIdentities_AuthenticationServices_AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "AuthenticationIdentities",
                column: "AuthenticationGrantTypeRefreshTokenId",
                principalSchema: "apps",
                principalTable: "AuthenticationServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
