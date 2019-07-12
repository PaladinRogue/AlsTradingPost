using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class AddApplicationHostUri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "apps",
                table: "Applications",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AddColumn<string>(
                name: "HostUri",
                schema: "apps",
                table: "Applications",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostUri",
                schema: "apps",
                table: "Applications");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "apps",
                table: "Applications",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 80);
        }
    }
}
