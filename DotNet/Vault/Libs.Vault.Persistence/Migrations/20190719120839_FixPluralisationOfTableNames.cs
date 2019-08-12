using Microsoft.EntityFrameworkCore.Migrations;

namespace PaladinRogue.Library.Vault.Persistence.Migrations
{
    public partial class FixPluralisationOfTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDataKey_Applications_ApplicationId",
                schema: "vault",
                table: "ApplicationDataKey");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationDataKey",
                schema: "vault",
                table: "ApplicationDataKey");

            migrationBuilder.RenameTable(
                name: "ApplicationDataKey",
                schema: "vault",
                newName: "ApplicationDataKeys",
                newSchema: "vault");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationDataKey_ApplicationId",
                schema: "vault",
                table: "ApplicationDataKeys",
                newName: "IX_ApplicationDataKeys_ApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationDataKeys",
                schema: "vault",
                table: "ApplicationDataKeys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDataKeys_Applications_ApplicationId",
                schema: "vault",
                table: "ApplicationDataKeys",
                column: "ApplicationId",
                principalSchema: "vault",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDataKeys_Applications_ApplicationId",
                schema: "vault",
                table: "ApplicationDataKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationDataKeys",
                schema: "vault",
                table: "ApplicationDataKeys");

            migrationBuilder.RenameTable(
                name: "ApplicationDataKeys",
                schema: "vault",
                newName: "ApplicationDataKey",
                newSchema: "vault");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationDataKeys_ApplicationId",
                schema: "vault",
                table: "ApplicationDataKey",
                newName: "IX_ApplicationDataKey_ApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationDataKey",
                schema: "vault",
                table: "ApplicationDataKey",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDataKey_Applications_ApplicationId",
                schema: "vault",
                table: "ApplicationDataKey",
                column: "ApplicationId",
                principalSchema: "vault",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
