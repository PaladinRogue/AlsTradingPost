using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class AddNotificationTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypeChannels",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChannelType = table.Column<int>(nullable: false),
                    NotificationTypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypeChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTypeChannels_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalSchema: "apps",
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChannelTemplates",
                schema: "apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    NotificationTypeChannelId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Template = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTypeChannels_NotificationTypeId",
                schema: "apps",
                table: "NotificationTypeChannels",
                column: "NotificationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelTemplates",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "NotificationTypeChannels",
                schema: "apps");

            migrationBuilder.DropTable(
                name: "NotificationTypes",
                schema: "apps");
        }
    }
}
