using Microsoft.EntityFrameworkCore.Migrations;

namespace PaladinRogue.Gateway.Persistence.Migrations
{
    public partial class UpdateSchemaNameToGateway : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gateway");

            migrationBuilder.RenameTable(
                name: "Applications",
                schema: "proxy",
                newName: "Applications",
                newSchema: "gateway");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "proxy");

            migrationBuilder.RenameTable(
                name: "Applications",
                schema: "gateway",
                newName: "Applications",
                newSchema: "proxy");
        }
    }
}
