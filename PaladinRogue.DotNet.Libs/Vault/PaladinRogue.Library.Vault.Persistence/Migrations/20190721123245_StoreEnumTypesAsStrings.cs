using Microsoft.EntityFrameworkCore.Migrations;

namespace PaladinRogue.Library.Vault.Persistence.Migrations
{
    public partial class StoreEnumTypesAsStrings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "vault",
                table: "SharedDataKeys",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "vault",
                table: "ApplicationDataKeys",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                schema: "vault",
                table: "SharedDataKeys",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                schema: "vault",
                table: "ApplicationDataKeys",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 80);
        }
    }
}
