using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AlsTradingPost.Persistence.Migrations
{
    public partial class ChangeIdentityToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Identities_IdentityId",
                schema: "dbo",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Identities_IdentityId",
                schema: "dbo",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Identities",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Players_IdentityId",
                schema: "dbo",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Admins_IdentityId",
                schema: "dbo",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                schema: "dbo",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                schema: "dbo",
                table: "Admins");

            migrationBuilder.AddColumn<Guid>(
                name: "Identity",
                schema: "dbo",
                table: "Players",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Identity",
                schema: "dbo",
                table: "Admins",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identity",
                schema: "dbo",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Identity",
                schema: "dbo",
                table: "Admins");

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityId",
                schema: "dbo",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityId",
                schema: "dbo",
                table: "Admins",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Players_IdentityId",
                schema: "dbo",
                table: "Players",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_IdentityId",
                schema: "dbo",
                table: "Admins",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Identities_IdentityId",
                schema: "dbo",
                table: "Admins",
                column: "IdentityId",
                principalSchema: "dbo",
                principalTable: "Identities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Identities_IdentityId",
                schema: "dbo",
                table: "Players",
                column: "IdentityId",
                principalSchema: "dbo",
                principalTable: "Identities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
