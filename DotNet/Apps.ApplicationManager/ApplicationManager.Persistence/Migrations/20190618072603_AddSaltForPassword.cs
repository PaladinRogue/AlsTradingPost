using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class AddSaltForPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Identities_IdentityId",
                schema: "apps",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_IdentityId",
                schema: "apps",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                schema: "apps",
                table: "Sessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdentityId",
                schema: "apps",
                table: "Sessions",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_IdentityId",
                schema: "apps",
                table: "Sessions",
                column: "IdentityId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Identities_IdentityId",
                schema: "apps",
                table: "Sessions",
                column: "IdentityId",
                principalSchema: "apps",
                principalTable: "Identities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthenticationIdentities_AuthenticationServices_AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Identities_IdentityId",
                schema: "apps",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_IdentityId",
                schema: "apps",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_AuthenticationIdentities_AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropColumn(
                name: "Salt",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropColumn(
                name: "AuthenticationGrantTypeRefreshTokenId",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdentityId",
                schema: "apps",
                table: "Sessions",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                schema: "apps",
                table: "Sessions",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_IdentityId",
                schema: "apps",
                table: "Sessions",
                column: "IdentityId",
                unique: true,
                filter: "[IdentityId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Identities_IdentityId",
                schema: "apps",
                table: "Sessions",
                column: "IdentityId",
                principalSchema: "apps",
                principalTable: "Identities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
