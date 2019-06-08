using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class UpdateChannelTemplateTableNameAndTypeDiscriminator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelTemplates",
                schema: "apps");

            migrationBuilder.CreateTable(
                name: "NotificationChannelTemplates",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    NotificationTypeChannelId = table.Column<Guid>(nullable: false),
                    Template = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationChannelTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationChannelTemplates_NotificationTypeChannels_NotificationTypeChannelId",
                        column: x => x.NotificationTypeChannelId,
                        principalSchema: "apps",
                        principalTable: "NotificationTypeChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationChannelTemplates_NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates",
                column: "NotificationTypeChannelId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationChannelTemplates",
                schema: "apps");

            migrationBuilder.CreateTable(
                name: "ChannelTemplates",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    NotificationTypeChannelId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Template = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelTemplates_NotificationTypeChannels_NotificationTypeChannelId",
                        column: x => x.NotificationTypeChannelId,
                        principalSchema: "apps",
                        principalTable: "NotificationTypeChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelTemplates_NotificationTypeChannelId",
                schema: "apps",
                table: "ChannelTemplates",
                column: "NotificationTypeChannelId",
                unique: true);
        }
    }
}
