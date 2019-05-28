using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class UpdateFieldLengthConstraintsForEncryptedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClientSecret",
                schema: "apps",
                table: "AuthenticationServices",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                schema: "apps",
                table: "AuthenticationServices",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClientSecret",
                schema: "apps",
                table: "AuthenticationServices",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                schema: "apps",
                table: "AuthenticationServices",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);
        }
    }
}
