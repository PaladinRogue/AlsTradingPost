using Microsoft.EntityFrameworkCore.Migrations;

namespace Vault.Persistence.Migrations
{
    public partial class StoreKeyTypesAsStringName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "vault",
                table: "SharedDataKeys",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "vault",
                table: "ApplicationDataKeys",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "vault",
                table: "SharedDataKeys",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "vault",
                table: "ApplicationDataKeys",
                newName: "Type");
        }
    }
}
