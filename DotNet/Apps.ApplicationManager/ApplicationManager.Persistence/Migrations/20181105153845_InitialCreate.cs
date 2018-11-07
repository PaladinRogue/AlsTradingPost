using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
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
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    SystemName = table.Column<string>(maxLength: 20, nullable: true)
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
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Type = table.Column<string>(nullable: false),
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
                name: "PasswordIdentites",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Identifier = table.Column<string>(maxLength: 40, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IdentityId = table.Column<Guid>(nullable: true),
                    AuthenticationGrantTypePasswordId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordIdentites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordIdentites_AuthenticationServices_AuthenticationGrantTypePasswordId",
                        column: x => x.AuthenticationGrantTypePasswordId,
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
                    RefreshToken = table.Column<string>(maxLength: 100, nullable: true),
                    IsRevoked = table.Column<bool>(nullable: false),
                    IdentityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identities",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identities_Sessions_Id",
                        column: x => x.Id,
                        principalSchema: "apps",
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordIdentites_AuthenticationGrantTypePasswordId",
                schema: "apps",
                table: "PasswordIdentites",
                column: "AuthenticationGrantTypePasswordId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordIdentites_IdentityId",
                schema: "apps",
                table: "PasswordIdentites",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_IdentityId",
                schema: "apps",
                table: "Sessions",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordIdentites_Identities_IdentityId",
                schema: "apps",
                table: "PasswordIdentites",
                column: "IdentityId",
                principalSchema: "apps",
                principalTable: "Identities",
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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Identities_Sessions_Id",
                schema: "apps",
                table: "Identities");

            migrationBuilder.DropTable(
                name: "Applications",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "PasswordIdentites",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "AuthenticationServices",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "Sessions",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "Identities",
                schema: "apps");
        }
    }
}
