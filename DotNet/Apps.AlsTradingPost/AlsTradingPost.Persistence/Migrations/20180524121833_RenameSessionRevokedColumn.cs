using Microsoft.EntityFrameworkCore.Migrations;

namespace AlsTradingPost.Persistence.Migrations
{
    public partial class RenameSessionRevokedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Revoked",
                schema: "dbo",
                table: "Sessions",
                newName: "IsRevoked");

            migrationBuilder.AlterColumn<string>(
                name: "DCI",
                schema: "dbo",
                table: "Traders",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Alias",
                schema: "dbo",
                table: "Traders",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRevoked",
                schema: "dbo",
                table: "Sessions",
                newName: "Revoked");

            migrationBuilder.AlterColumn<string>(
                name: "DCI",
                schema: "dbo",
                table: "Traders",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Alias",
                schema: "dbo",
                table: "Traders",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);
        }
    }
}
