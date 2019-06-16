using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class MakeIdentityToIdentitiesReletionshipMandatory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthenticationIdentities_Identities_IdentityId",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdentityId",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthenticationIdentities_Identities_IdentityId",
                schema: "apps",
                table: "AuthenticationIdentities",
                column: "IdentityId",
                principalSchema: "apps",
                principalTable: "Identities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthenticationIdentities_Identities_IdentityId",
                schema: "apps",
                table: "AuthenticationIdentities");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdentityId",
                schema: "apps",
                table: "AuthenticationIdentities",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_AuthenticationIdentities_Identities_IdentityId",
                schema: "apps",
                table: "AuthenticationIdentities",
                column: "IdentityId",
                principalSchema: "apps",
                principalTable: "Identities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
