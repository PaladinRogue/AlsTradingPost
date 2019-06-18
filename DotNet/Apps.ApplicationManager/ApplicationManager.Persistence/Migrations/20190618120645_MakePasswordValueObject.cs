using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class MakePasswordValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                newName: "ProtectedPassword_Salt");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "apps",
                table: "AuthenticationIdentities",
                newName: "ProtectedPassword_Hash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProtectedPassword_Salt",
                schema: "apps",
                table: "AuthenticationIdentities",
                newName: "Salt");

            migrationBuilder.RenameColumn(
                name: "ProtectedPassword_Hash",
                schema: "apps",
                table: "AuthenticationIdentities",
                newName: "Password");
        }
    }
}
