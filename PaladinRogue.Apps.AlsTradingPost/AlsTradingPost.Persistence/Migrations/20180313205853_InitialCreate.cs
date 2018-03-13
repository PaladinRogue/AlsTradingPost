using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AlsTradingPost.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Identities",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    IdentityId = table.Column<Guid>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_Identities_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "dbo",
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DCI = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IdentityId = table.Column<Guid>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Identities_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "dbo",
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Class = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PlayerId = table.Column<Guid>(nullable: true),
                    Race = table.Column<string>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "dbo",
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CharacterId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ForTrade = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Rarity = table.Column<string>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalSchema: "dbo",
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_IdentityId",
                schema: "dbo",
                table: "Admins",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PlayerId",
                schema: "dbo",
                table: "Characters",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CharacterId",
                schema: "dbo",
                table: "Items",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_IdentityId",
                schema: "dbo",
                table: "Players",
                column: "IdentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Characters",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Players",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Identities",
                schema: "dbo");
        }
    }
}
