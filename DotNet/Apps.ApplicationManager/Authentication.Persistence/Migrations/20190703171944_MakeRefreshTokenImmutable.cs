using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication.Persistence.Migrations
{
    public partial class MakeRefreshTokenImmutable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                schema: "apps",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_SessionId",
                schema: "apps",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "apps",
                table: "RefreshTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                schema: "apps",
                table: "RefreshTokens",
                column: "SessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                schema: "apps",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "apps",
                table: "RefreshTokens",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                schema: "apps",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_SessionId",
                schema: "apps",
                table: "RefreshTokens",
                column: "SessionId",
                unique: true);
        }
    }
}
