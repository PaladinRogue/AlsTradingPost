﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace PaladinRogue.Authentication.Persistence.Migrations
{
    public partial class AddAppAcccessToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppAccessToken",
                schema: "apps",
                table: "AuthenticationServices",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppAccessToken",
                schema: "apps",
                table: "AuthenticationServices");
        }
    }
}
