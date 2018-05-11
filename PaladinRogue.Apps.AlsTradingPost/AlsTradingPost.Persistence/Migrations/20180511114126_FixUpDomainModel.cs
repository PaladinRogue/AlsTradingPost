using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AlsTradingPost.Persistence.Migrations
{
    public partial class FixUpDomainModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Players_PlayerId",
                schema: "dbo",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "ItemReferenceData",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Players",
                schema: "dbo");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                schema: "dbo",
                table: "Characters",
                newName: "TraderId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_PlayerId",
                schema: "dbo",
                table: "Characters",
                newName: "IX_Characters_TraderId");

            migrationBuilder.AlterColumn<string>(
                name: "Race",
                schema: "dbo",
                table: "Characters",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Characters",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Level",
                schema: "dbo",
                table: "Characters",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Class",
                schema: "dbo",
                table: "Characters",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                schema: "dbo",
                table: "Audits",
                rowVersion: true,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MagicItemTemplates",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Rarity = table.Column<int>(nullable: false),
                    Verified = table.Column<bool>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagicItemTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traders",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Alias = table.Column<string>(maxLength: 50, nullable: false),
                    DCI = table.Column<string>(maxLength: 50, nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MagicItems",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CharacterId = table.Column<Guid>(nullable: true),
                    ForTrade = table.Column<bool>(nullable: false),
                    MagicItemTemplateId = table.Column<Guid>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagicItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MagicItems_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalSchema: "dbo",
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MagicItems_MagicItemTemplates_MagicItemTemplateId",
                        column: x => x.MagicItemTemplateId,
                        principalSchema: "dbo",
                        principalTable: "MagicItemTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MagicItems_CharacterId",
                schema: "dbo",
                table: "MagicItems",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_MagicItems_MagicItemTemplateId",
                schema: "dbo",
                table: "MagicItems",
                column: "MagicItemTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Traders_TraderId",
                schema: "dbo",
                table: "Characters",
                column: "TraderId",
                principalSchema: "dbo",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Traders_TraderId",
                schema: "dbo",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "MagicItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Traders",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MagicItemTemplates",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "dbo",
                table: "Audits");

            migrationBuilder.RenameColumn(
                name: "TraderId",
                schema: "dbo",
                table: "Characters",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_TraderId",
                schema: "dbo",
                table: "Characters",
                newName: "IX_Characters_PlayerId");

            migrationBuilder.AlterColumn<string>(
                name: "Race",
                schema: "dbo",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                schema: "dbo",
                table: "Characters",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<string>(
                name: "Class",
                schema: "dbo",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ItemReferenceData",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Verified = table.Column<bool>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemReferenceData", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Players",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Alias = table.Column<string>(maxLength: 50, nullable: true),
                    DCI = table.Column<string>(maxLength: 50, nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_CharacterId",
                schema: "dbo",
                table: "Items",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Players_PlayerId",
                schema: "dbo",
                table: "Characters",
                column: "PlayerId",
                principalSchema: "dbo",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
