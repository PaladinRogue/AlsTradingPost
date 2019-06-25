using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class AddPasswordAuthenticationServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                INSERT INTO [apps].[AuthenticationServices]
                           ([Id]
                           ,[Type]
                           ,[Version])
                     VALUES
                           ('D5493BEA-EED6-4021-A671-2516FEFEB23B'
                           ,'PASSWORD'
                           ,0)
                GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
