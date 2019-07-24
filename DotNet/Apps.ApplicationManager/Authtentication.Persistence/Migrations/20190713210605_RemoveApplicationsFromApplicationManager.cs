using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authtentication.Persistence.Migrations
{
    public partial class RemoveApplicationsFromApplicationManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications",
                schema: "apps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HostUri = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    SystemName = table.Column<string>(maxLength: 20, nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });
        }
    }
}
