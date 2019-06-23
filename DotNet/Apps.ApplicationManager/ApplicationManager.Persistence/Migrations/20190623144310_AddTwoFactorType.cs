using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class AddTwoFactorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProtectedPassword_Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                newName: "PasswordHash_Salt");

            migrationBuilder.RenameColumn(
                name: "ProtectedPassword_Hash",
                schema: "apps",
                table: "AuthenticationIdentities",
                newName: "PasswordHash_Hash");

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

            migrationBuilder.AddColumn<string>(
                name: "TwoFactorAuthenticationType",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoFactorAuthenticationType",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.RenameColumn(
                name: "PasswordHash_Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                newName: "ProtectedPassword_Salt");

            migrationBuilder.RenameColumn(
                name: "PasswordHash_Hash",
                schema: "apps",
                table: "AuthenticationIdentities",
                newName: "ProtectedPassword_Hash");

            migrationBuilder.AlterColumn<string>(
                name: "ProtectedPassword_Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProtectedPassword_Hash",
                schema: "apps",
                table: "AuthenticationIdentities",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
