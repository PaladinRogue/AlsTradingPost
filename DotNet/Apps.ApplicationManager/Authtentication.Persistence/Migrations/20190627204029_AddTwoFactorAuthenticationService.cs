using Microsoft.EntityFrameworkCore.Migrations;

namespace Authtentication.Persistence.Migrations
{
    public partial class AddTwoFactorAuthenticationService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [apps].[AuthenticationServices]
                           ([Id]
                           ,[Type]
                           ,[Version])
                     VALUES
                           ('2ECFEC94-B7E3-4B09-96B8-3F0C08E27A4F'
                           ,'REFRESH_TOKEN'
                           ,0)
                GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
