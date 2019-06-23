using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationManager.Persistence.Migrations
{
    public partial class AddForgotPasswordAndConfirmIdentityNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM [apps].[NotificationChannelTemplates]
                      WHERE [Id] = '5D0371BA-518B-4DB4-98C1-1EBD42FB3B46'

                DELETE FROM [apps].[NotificationTypeChannels]
                      WHERE [Id] = 'DFF6BCEB-36A6-4550-B150-FC8536C14E75'

                DELETE FROM [apps].[NotificationTypes]
                      WHERE [Id] = 'ED3AB96C-8F45-45E8-8880-D9F46C075BEC'

                INSERT INTO [apps].[NotificationTypes]
                    ([Id]
                    ,[Type])
                    VALUES
                    ('4BEC8097-5BF4-4C96-9A1C-20097F316CFC'
                    ,'CONFIRM_IDENTITY')

                INSERT INTO [apps].[NotificationTypeChannels]
                    ([Id]
                    ,[ChannelType]
                    ,[NotificationTypeId])
                    VALUES
                    ('1F3730FD-C6B2-409B-90A8-F1EA3D15FEC1'
                    ,0
                    ,'4BEC8097-5BF4-4C96-9A1C-20097F316CFC')

                INSERT INTO [apps].[NotificationChannelTemplates]
                    ([Id]
                    ,[Type]
                    ,[NotificationTypeChannelId]
                    ,[Template]
                    ,[Subject])
                    VALUES
                    ('A4B21460-35E9-4653-A494-ECC4C16F4E49'
                    ,'EMAIL'
                    ,'1F3730FD-C6B2-409B-90A8-F1EA3D15FEC1'
                    ,'<html><head></head><body>
                    <p>{TOKEN}</p>
                    </body></html>'
                    ,'Please confirm your email')

                INSERT INTO [apps].[NotificationTypes]
                    ([Id]
                    ,[Type])
                    VALUES
                    ('3904F6C1-CF97-4B9B-BA0F-DFC76E5B44D2'
                    ,'FORGOT_PASSWORD')

                INSERT INTO [apps].[NotificationTypeChannels]
                    ([Id]
                    ,[ChannelType]
                    ,[NotificationTypeId])
                    VALUES
                    ('782F8B7C-916A-47E4-A18F-8D15E9B7F104'
                    ,0
                    ,'3904F6C1-CF97-4B9B-BA0F-DFC76E5B44D2')

                INSERT INTO [apps].[NotificationChannelTemplates]
                    ([Id]
                    ,[Type]
                    ,[NotificationTypeChannelId]
                    ,[Template]
                    ,[Subject])
                    VALUES
                    ('4AA6E188-DFE1-4146-8981-F76F464729AA'
                    ,'EMAIL'
                    ,'782F8B7C-916A-47E4-A18F-8D15E9B7F104'
                    ,'<html><head></head><body>
                    <p>{TOKEN}</p>
                    </body></html>'
                    ,'Forgotten password')

                GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
