using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class IdentityDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdentityId",
                schema: "apps",
                table: "PasswordIdentity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Session",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefreshToken = table.Column<string>(maxLength: 100, nullable: true),
                    IsRevoked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identities",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SessionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identities_Session_SessionId",
                        column: x => x.SessionId,
                        principalSchema: "apps",
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordIdentity_IdentityId",
                schema: "apps",
                table: "PasswordIdentity",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Identities_SessionId",
                schema: "apps",
                table: "Identities",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordIdentity_Identities_IdentityId",
                schema: "apps",
                table: "PasswordIdentity",
                column: "IdentityId",
                principalSchema: "apps",
                principalTable: "Identities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Identities_Id",
                schema: "apps",
                table: "Session",
                column: "Id",
                principalSchema: "apps",
                principalTable: "Identities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordIdentity_Identities_IdentityId",
                schema: "apps",
                table: "PasswordIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_Identities_Session_SessionId",
                schema: "apps",
                table: "Identities");

            migrationBuilder.DropTable(
                name: "Session",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "Identities",
                schema: "apps");

            migrationBuilder.DropIndex(
                name: "IX_PasswordIdentity_IdentityId",
                schema: "apps",
                table: "PasswordIdentity");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                schema: "apps",
                table: "PasswordIdentity");
        }
    }
}
