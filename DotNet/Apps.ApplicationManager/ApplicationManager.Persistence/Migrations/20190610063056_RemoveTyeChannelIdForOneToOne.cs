using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class RemoveTyeChannelIdForOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationChannelTemplates_NotificationTypeChannels_NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates");

            migrationBuilder.DropIndex(
                name: "IX_NotificationChannelTemplates_NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates");

            migrationBuilder.AlterColumn<Guid>(
                name: "NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_NotificationChannelTemplates_NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates",
                column: "NotificationTypeChannelId",
                unique: true,
                filter: "[NotificationTypeChannelId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationChannelTemplates_NotificationTypeChannels_NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates",
                column: "NotificationTypeChannelId",
                principalSchema: "apps",
                principalTable: "NotificationTypeChannels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationChannelTemplates_NotificationTypeChannels_NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates");

            migrationBuilder.DropIndex(
                name: "IX_NotificationChannelTemplates_NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates");

            migrationBuilder.AlterColumn<Guid>(
                name: "NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationChannelTemplates_NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates",
                column: "NotificationTypeChannelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationChannelTemplates_NotificationTypeChannels_NotificationTypeChannelId",
                schema: "apps",
                table: "NotificationChannelTemplates",
                column: "NotificationTypeChannelId",
                principalSchema: "apps",
                principalTable: "NotificationTypeChannels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
