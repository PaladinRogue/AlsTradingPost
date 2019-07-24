using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vault.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "vault");

            migrationBuilder.CreateTable(
                name: "Applications",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    SystemName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SharedDataKeys",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedDataKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationDataKey",
                schema: "vault",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 1024, nullable: false),
                    ApplicationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDataKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationDataKey_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "vault",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDataKey_ApplicationId",
                schema: "vault",
                table: "ApplicationDataKey",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationDataKey",
                schema: "vault");

            migrationBuilder.DropTable(
                name: "SharedDataKeys",
                schema: "vault");

            migrationBuilder.DropTable(
                name: "Applications",
                schema: "vault");
        }
    }
}
