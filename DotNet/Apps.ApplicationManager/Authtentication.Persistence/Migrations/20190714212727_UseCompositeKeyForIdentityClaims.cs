using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authtentication.Persistence.Migrations
{
    public partial class UseCompositeKeyForIdentityClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Claims",
                schema: "apps",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "apps",
                table: "Claims");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Claims",
                schema: "apps",
                table: "Claims",
                columns: new[] { "Type", "IdentityId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Claims",
                schema: "apps",
                table: "Claims");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "apps",
                table: "Claims",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Claims",
                schema: "apps",
                table: "Claims",
                column: "Id");
        }
    }
}
