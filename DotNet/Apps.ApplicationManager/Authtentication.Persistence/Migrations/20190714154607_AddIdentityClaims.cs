using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authtentication.Persistence.Migrations
{
    public partial class AddIdentityClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claims",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 80, nullable: false),
                    Value = table.Column<string>(maxLength: 80, nullable: false),
                    IdentityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Identities_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "apps",
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_IdentityId",
                schema: "apps",
                table: "Claims",
                column: "IdentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims",
                schema: "apps");
        }
    }
}
