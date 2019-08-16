using Microsoft.EntityFrameworkCore.Migrations;

namespace PaladinRogue.Authentication.Persistence.Migrations
{
    public partial class UpdateSchemaName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "authentication");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "apps",
                newName: "Users",
                newSchema: "authentication");

            migrationBuilder.RenameTable(
                name: "Sessions",
                schema: "apps",
                newName: "Sessions",
                newSchema: "authentication");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                schema: "apps",
                newName: "RefreshTokens",
                newSchema: "authentication");

            migrationBuilder.RenameTable(
                name: "ReferenceDataValues",
                schema: "apps",
                newName: "ReferenceDataValues",
                newSchema: "authentication");

            migrationBuilder.RenameTable(
                name: "ReferenceDataTypes",
                schema: "apps",
                newName: "ReferenceDataTypes",
                newSchema: "authentication");

            migrationBuilder.RenameTable(
                name: "NotificationTypes",
                schema: "apps",
                newName: "NotificationTypes",
                newSchema: "authentication");

            migrationBuilder.RenameTable(
                name: "NotificationTypeChannels",
                schema: "apps",
                newName: "NotificationTypeChannels",
                newSchema: "authentication");

            migrationBuilder.RenameTable(
                name: "NotificationChannelTemplates",
                schema: "apps",
                newName: "NotificationChannelTemplates",
                newSchema: "authentication");

            migrationBuilder.RenameTable(
                name: "Identities",
                schema: "apps",
                newName: "Identities",
                newSchema: "authentication");

            migrationBuilder.RenameTable(
                name: "Claims",
                schema: "apps",
                newName: "Claims",
                newSchema: "authentication");

            migrationBuilder.RenameTable(
                name: "AuthenticationServices",
                schema: "apps",
                newName: "AuthenticationServices",
                newSchema: "authentication");

            migrationBuilder.RenameTable(
                name: "AuthenticationIdentities",
                schema: "apps",
                newName: "AuthenticationIdentities",
                newSchema: "authentication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "apps");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "authentication",
                newName: "Users",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "Sessions",
                schema: "authentication",
                newName: "Sessions",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                schema: "authentication",
                newName: "RefreshTokens",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "ReferenceDataValues",
                schema: "authentication",
                newName: "ReferenceDataValues",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "ReferenceDataTypes",
                schema: "authentication",
                newName: "ReferenceDataTypes",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "NotificationTypes",
                schema: "authentication",
                newName: "NotificationTypes",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "NotificationTypeChannels",
                schema: "authentication",
                newName: "NotificationTypeChannels",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "NotificationChannelTemplates",
                schema: "authentication",
                newName: "NotificationChannelTemplates",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "Identities",
                schema: "authentication",
                newName: "Identities",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "Claims",
                schema: "authentication",
                newName: "Claims",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "AuthenticationServices",
                schema: "authentication",
                newName: "AuthenticationServices",
                newSchema: "apps");

            migrationBuilder.RenameTable(
                name: "AuthenticationIdentities",
                schema: "authentication",
                newName: "AuthenticationIdentities",
                newSchema: "apps");
        }
    }
}
