using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication.Persistence.Migrations
{
    public partial class MoveEmailAddressToPasswordIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddressHash",
                schema: "apps",
                table: "Identities");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenHash_Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenHash_Hash",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash_Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash_Hash",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddressHash",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddressHash",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddressHash",
                schema: "apps",
                table: "Identities",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenHash_Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenHash_Hash",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash_Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash_Hash",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);
        }
    }
}
