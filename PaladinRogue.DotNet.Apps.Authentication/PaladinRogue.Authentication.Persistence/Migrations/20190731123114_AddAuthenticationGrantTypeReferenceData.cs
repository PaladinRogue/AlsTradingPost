using Microsoft.EntityFrameworkCore.Migrations;

namespace PaladinRogue.Authentication.Persistence.Migrations
{
    public partial class AddAuthenticationGrantTypeReferenceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DECLARE @referenceDataTypeId UNIQUEIDENTIFIER = '48CC4F9F-6A84-4691-B348-6CC5D2CF1670';

                INSERT INTO [authentication].[ReferenceDataTypes]
                           ([Id]
                           ,[Version]
                           ,[Type])
                     VALUES
                           (@referenceDataTypeId
                           ,0
                           ,'ClientCredentialAuthenticationGrantType')

                INSERT INTO [authentication].[ReferenceDataValues]
                           ([Id]
                           ,[Code]
                           ,[ReferenceDataTypeId])
                     VALUES
                           ('32F95D69-18E8-44A7-9F28-DEEBD3EA626B'
                           ,'Facebook'
                           ,@referenceDataTypeId),
                           ('5CDF76BF-E7FE-40FB-9A12-4757F3F8713D'
                           ,'Google'
                           ,@referenceDataTypeId)
                "
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}