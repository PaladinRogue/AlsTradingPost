using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class AddEmailHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                schema: "apps",
                table: "Identities");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddressHash",
                schema: "apps",
                table: "Identities",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddressHash",
                schema: "apps",
                table: "Identities");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                schema: "apps",
                table: "Identities",
                maxLength: 1024,
                nullable: true);
        }
    }
}
