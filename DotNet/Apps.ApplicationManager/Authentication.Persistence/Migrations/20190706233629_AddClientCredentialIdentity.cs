using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication.Persistence.Migrations
{
    public partial class AddClientCredentialIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthenticationGrantTypeClientCredentialId",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentifierHash",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationIdentities_AuthenticationGrantTypeClientCredentialId",
                schema: "apps",
                table: "AuthenticationIdentities",
                column: "AuthenticationGrantTypeClientCredentialId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthenticationIdentities_AuthenticationServices_AuthenticationGrantTypeClientCredentialId",
                schema: "apps",
                table: "AuthenticationIdentities",
                column: "AuthenticationGrantTypeClientCredentialId",
                principalSchema: "apps",
                principalTable: "AuthenticationServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthenticationIdentities_AuthenticationServices_AuthenticationGrantTypeClientCredentialId",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropIndex(
                name: "IX_AuthenticationIdentities_AuthenticationGrantTypeClientCredentialId",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropColumn(
                name: "AuthenticationGrantTypeClientCredentialId",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.DropColumn(
                name: "IdentifierHash",
                schema: "apps",
                table: "AuthenticationIdentities");
        }
    }
}
