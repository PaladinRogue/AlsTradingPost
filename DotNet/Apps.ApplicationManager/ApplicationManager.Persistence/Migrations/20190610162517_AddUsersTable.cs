using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class AddUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Identities_IdentityId",
                schema: "apps",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_IdentityId",
                schema: "apps",
                table: "Sessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdentityId",
                schema: "apps",
                table: "Sessions",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IdentityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Identities_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "apps",
                        principalTable: "Identities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_IdentityId",
                schema: "apps",
                table: "Sessions",
                column: "IdentityId",
                unique: true,
                filter: "[IdentityId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityId",
                schema: "apps",
                table: "Users",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Identities_IdentityId",
                schema: "apps",
                table: "Sessions",
                column: "IdentityId",
                principalSchema: "apps",
                principalTable: "Identities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Identities_IdentityId",
                schema: "apps",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "apps");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_IdentityId",
                schema: "apps",
                table: "Sessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdentityId",
                schema: "apps",
                table: "Sessions",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_IdentityId",
                schema: "apps",
                table: "Sessions",
                column: "IdentityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Identities_IdentityId",
                schema: "apps",
                table: "Sessions",
                column: "IdentityId",
                principalSchema: "apps",
                principalTable: "Identities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
