using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication.Persistence.Migrations
{
    public partial class HashRefreshTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.AddColumn<DateTime>(
                name: "TwoFactorAuthenticationIdentity_TokenExpiry",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokenHash_Hash",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokenHash_Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true);
        }
    }
}
