using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Persistence;

namespace AlsTradingPost.Setup.Infrastructure.DbInitializer
{
    public static class DbInitializer
    {
        private static readonly IList<ItemReferenceData> ItemReferenceData = new List<ItemReferenceData>
        {
            new ItemReferenceData { Id = Guid.Parse("370A1B2F-319E-4DBC-9F93-213EDCEE06BD"), Name = "Item01", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("4C3C1573-D3CE-40C2-80FB-4CDE0A1FF4BC"), Name = "Item02", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("ADF73E6C-A838-4D5B-9326-A7A605E2F452"), Name = "Item03", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("87F9FC34-1668-4D50-8ADC-8A11178006B9"), Name = "Item04", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("A1DD21CA-A027-4699-A0D1-A80BEDF97E2F"), Name = "Item05", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("9836558E-0B04-40A9-A8F4-20DAEE766A03"), Name = "Item06", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("FD79B383-C76C-4A52-ADB6-2B5731CD016B"), Name = "Item07", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("8A7023A5-70A8-4528-9251-13205D0F9E33"), Name = "Item08", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("EB74612E-A18B-4FAA-A986-B2AB00D47559"), Name = "Item09", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("837BB6CD-DBEF-44B0-92A9-54033BA9A98C"), Name = "Item10", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("D61ECDAA-5AE1-4718-A20C-DCB08D442A5F"), Name = "Item11", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("7B156B80-EC72-4187-BB25-EE1343D84683"), Name = "Item12", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("96BDB84C-B0B1-4AB8-9C0A-35C24664745A"), Name = "Item13", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("94E6FB6D-9A4F-45D8-A616-FC49F1F6BD88"), Name = "Item14", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("4972430D-447C-44DE-91D2-E513D495577A"), Name = "Item15", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("A4B4B646-4FDD-49B7-84D8-21F195BA2C01"), Name = "Item16", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("A1885135-58E2-45D2-83FE-71EEA99D61F1"), Name = "Item17", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("00E35E07-7528-4DAF-BF5D-A14CFA8D50F2"), Name = "Item18", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("84725ACA-8D1A-462D-BCF9-C966455C2256"), Name = "Item19", Verified = true },
            new ItemReferenceData { Id = Guid.Parse("9FED7706-C684-4026-91AF-008290C02117"), Name = "Item20", Verified = true }
        };

        public static void Seed(AlsTradingPostDbContext context)
        {
            if (!context.ItemReferenceData.Any())
            {
                context.ItemReferenceData.AddOrUpdate(ItemReferenceData);
            }

            context.SaveChanges();
        }
    }
}
