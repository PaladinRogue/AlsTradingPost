using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class RemoveApplicationAuthServiceLinkAddPasswordIdentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationAuthenticationServices",
                schema: "dbo");

            migrationBuilder.EnsureSchema(
                name: "apps");

            migrationBuilder.RenameTable(
                name: "AuthenticationServices",
                schema: "dbo",
                newName: "AuthenticationServices",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "Applications",
                schema: "dbo",
                newName: "Applications",
                newSchema: "apps");

            migrationBuilder.CreateTable(
                name: "PasswordIdentity",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Identifier = table.Column<string>(maxLength: 40, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    AuthenticationGrantTypePasswordId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordIdentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordIdentity_AuthenticationServices_AuthenticationGrantTypePasswordId",
                        column: x => x.AuthenticationGrantTypePasswordId,
                        principalSchema: "apps",
                        principalTable: "AuthenticationServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordIdentity_AuthenticationGrantTypePasswordId",
                schema: "apps",
                table: "PasswordIdentity",
                column: "AuthenticationGrantTypePasswordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordIdentity",
                schema: "apps");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "AuthenticationServices",
                schema: "apps",
                newName: "AuthenticationServices",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Applications",
                schema: "apps",
                newName: "Applications",
                newSchema: "dbo");

            migrationBuilder.CreateTable(
                name: "ApplicationAuthenticationServices",
                schema: "dbo",
                columns: table => new
                {
                    ApplicationId = table.Column<Guid>(nullable: false),
                    AuthenticationServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationAuthenticationServices", x => new { x.ApplicationId, x.AuthenticationServiceId });
                    table.ForeignKey(
                        name: "FK_ApplicationAuthenticationServices_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "dbo",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationAuthenticationServices_AuthenticationServices_AuthenticationServiceId",
                        column: x => x.AuthenticationServiceId,
                        principalSchema: "dbo",
                        principalTable: "AuthenticationServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationAuthenticationServices_AuthenticationServiceId",
                schema: "dbo",
                table: "ApplicationAuthenticationServices",
                column: "AuthenticationServiceId");
        }
    }
}
