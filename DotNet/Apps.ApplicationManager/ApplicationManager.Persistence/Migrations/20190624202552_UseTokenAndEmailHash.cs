using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class UseTokenAndEmailHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                schema: "apps",
                table: "Identities",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TwoFactorAuthenticationType",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                schema: "apps",
                table: "Identities");

            migrationBuilder.AlterColumn<string>(
                name: "TwoFactorAuthenticationType",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
