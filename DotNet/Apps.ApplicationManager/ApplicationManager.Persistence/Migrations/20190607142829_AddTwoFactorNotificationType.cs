using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class AddTwoFactorNotificationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                INSERT INTO [apps].[NotificationTypes]
                    ([Id]
                    ,[Type])
                    VALUES
                    ('ED3AB96C-8F45-45E8-8880-D9F46C075BEC'
                    ,'{Domain.NotificationTypes.NotificationTypes.EmailTwoFactorAuthentication}')

                INSERT INTO [apps].[NotificationTypeChannels]
                    ([Id]
                    ,[ChannelType]
                    ,[NotificationTypeId])
                    VALUES
                    ('DFF6BCEB-36A6-4550-B150-FC8536C14E75'
                    ,0
                    ,'ED3AB96C-8F45-45E8-8880-D9F46C075BEC')

                INSERT INTO [apps].[NotificationChannelTemplates]
                    ([Id]
                    ,[Type]
                    ,[NotificationTypeChannelId]
                    ,[Template]
                    ,[Subject])
                    VALUES
                    ('5D0371BA-518B-4DB4-98C1-1EBD42FB3B46'
                    ,'EMAIL'
                    ,'DFF6BCEB-36A6-4550-B150-FC8536C14E75'
                    ,'<html><head></head><body>
                    <p>{{TOKEN}}</p>
                    </body></html>'
                    ,'Please confirm your email')

                GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
