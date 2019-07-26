using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication.Persistence.Migrations
{
    public partial class AddConfirmAndForgotPasswordNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [apps].[NotificationTypes]
                    ([Id]
                    ,[Type]
                    ,[Version])
                    VALUES
                    ('4BEC8097-5BF4-4C96-9A1C-20097F316CFC'
                    ,'CONFIRM_IDENTITY'
                    ,0)

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
                    ,[Type]
                    ,[Version])
                    VALUES
                    ('3904F6C1-CF97-4B9B-BA0F-DFC76E5B44D2'
                    ,'FORGOT_PASSWORD'
                    ,0)

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
