using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Applications",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthenticationServices",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    ClientSecret = table.Column<string>(nullable: true),
                    ClientGrantAccessTokenUrl = table.Column<string>(nullable: true),
                    GrantAccessTokenUrl = table.Column<string>(nullable: true),
                    ValidateAccessTokenUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationServices", x => x.Id);
                });

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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationAuthenticationServices_AuthenticationServiceId",
                schema: "dbo",
                table: "ApplicationAuthenticationServices",
                column: "AuthenticationServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationAuthenticationServices",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Applications",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AuthenticationServices",
                schema: "dbo");
        }
    }
}
