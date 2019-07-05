using System;
using System.Reflection;
using ApplicationManager.Domain.Identities;
using Common.Domain.DataProtection;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Repositories;

namespace ApplicationManager.Persistence.Identities
{
    public class IdentityCommandRepository : CommandRepository<Identity>
    {
        public IdentityCommandRepository(DbContext context) : base(context)
        {
        }

        [Obsolete("Only needed as EF Core 2.2 does not track nested owned entities properly")]
        public override void Update(Identity entity)
        {
            PropertyInfo tokenHashPropertyInfo = typeof(RefreshToken).GetProperty("TokenHash", BindingFlags.NonPublic | BindingFlags.Instance);
            HashSet refreshTokenTokenHash = tokenHashPropertyInfo.GetValue(entity.Session.RefreshToken) as HashSet;

            Context.Entry(entity).Reference(x =>  x.Session)
                .TargetEntry.Reference(x => x.RefreshToken)
                .TargetEntry.Reference("TokenHash")
                .CurrentValue = refreshTokenTokenHash;

            RepositoryHelper.Update(Context.Set<Identity>(), Context, entity);
        }
    }
}