using Microsoft.EntityFrameworkCore.Migrations;

namespace ReverseProxy.Persistence.Migrations
{
    public partial class UpdateSchemaName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "proxy");

            migrationBuilder.RenameTable(
                name: "Applications",
                schema: "apps",
                newName: "Applications",
                newSchema: "proxy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "apps");

            migrationBuilder.RenameTable(
                name: "Applications",
                schema: "proxy",
                newName: "Applications",
                newSchema: "apps");
        }
    }
}
