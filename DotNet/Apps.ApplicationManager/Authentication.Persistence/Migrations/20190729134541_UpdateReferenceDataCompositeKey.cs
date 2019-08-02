using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication.Persistence.Migrations
{
    public partial class UpdateReferenceDataCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ReferenceDataValues_Code",
                schema: "authentication",
                table: "ReferenceDataValues");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ReferenceDataValues_Code_ReferenceDataTypeId",
                schema: "authentication",
                table: "ReferenceDataValues",
                columns: new[] { "Code", "ReferenceDataTypeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ReferenceDataValues_Code_ReferenceDataTypeId",
                schema: "authentication",
                table: "ReferenceDataValues");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ReferenceDataValues_Code",
                schema: "authentication",
                table: "ReferenceDataValues",
                column: "Code");
        }
    }
}
