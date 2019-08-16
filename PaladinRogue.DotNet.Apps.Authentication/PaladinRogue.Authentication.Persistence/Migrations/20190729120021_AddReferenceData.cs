using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaladinRogue.Authentication.Persistence.Migrations
{
    public partial class AddReferenceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Claims",
                schema: "apps",
                table: "Claims");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Claims",
                schema: "apps",
                table: "Claims",
                columns: new[] { "Type", "IdentityId" });

            migrationBuilder.CreateTable(
                name: "ReferenceDataTypes",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceDataTypes", x => x.Id);
                    table.UniqueConstraint("AK_ReferenceDataTypes_Type", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceDataValues",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 80, nullable: false),
                    ReferenceDataTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceDataValues", x => x.Id);
                    table.UniqueConstraint("AK_ReferenceDataValues_Code", x => x.Code);
                    table.ForeignKey(
                        name: "FK_ReferenceDataValues_ReferenceDataTypes_ReferenceDataTypeId",
                        column: x => x.ReferenceDataTypeId,
                        principalSchema: "apps",
                        principalTable: "ReferenceDataTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceDataValues_ReferenceDataTypeId",
                schema: "apps",
                table: "ReferenceDataValues",
                column: "ReferenceDataTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReferenceDataValues",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "ReferenceDataTypes",
                schema: "apps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Claims",
                schema: "apps",
                table: "Claims");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Claims",
                schema: "apps",
                table: "Claims",
                column: "Type");
        }
    }
}
