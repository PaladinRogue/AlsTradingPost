using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AlsTradingPost.Persistence.Migrations
{
    public partial class AddUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_PersonalDetails_PersonalDetailsId",
                schema: "dbo",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_PersonalDetails_PersonalDetailsId",
                schema: "dbo",
                table: "Players");

            migrationBuilder.DropTable(
                name: "PersonalDetails",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Players_PersonalDetailsId",
                schema: "dbo",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Admins_PersonalDetailsId",
                schema: "dbo",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Identity",
                schema: "dbo",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PersonalDetailsId",
                schema: "dbo",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Identity",
                schema: "dbo",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "PersonalDetailsId",
                schema: "dbo",
                table: "Admins");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    IdentityId = table.Column<Guid>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    PictureUrl = table.Column<string>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");

            migrationBuilder.AddColumn<Guid>(
                name: "Identity",
                schema: "dbo",
                table: "Players",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalDetailsId",
                schema: "dbo",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Identity",
                schema: "dbo",
                table: "Admins",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalDetailsId",
                schema: "dbo",
                table: "Admins",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonalDetails",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PictureUrl = table.Column<string>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_PersonalDetailsId",
                schema: "dbo",
                table: "Players",
                column: "PersonalDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_PersonalDetailsId",
                schema: "dbo",
                table: "Admins",
                column: "PersonalDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_PersonalDetails_PersonalDetailsId",
                schema: "dbo",
                table: "Admins",
                column: "PersonalDetailsId",
                principalSchema: "dbo",
                principalTable: "PersonalDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_PersonalDetails_PersonalDetailsId",
                schema: "dbo",
                table: "Players",
                column: "PersonalDetailsId",
                principalSchema: "dbo",
                principalTable: "PersonalDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
