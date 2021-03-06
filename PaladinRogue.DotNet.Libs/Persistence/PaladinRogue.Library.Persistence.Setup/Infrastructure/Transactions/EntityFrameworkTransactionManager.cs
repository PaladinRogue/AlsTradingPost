﻿using Microsoft.EntityFrameworkCore;
using PaladinRogue.Library.Core.Application.Transactions;

namespace PaladinRogue.Library.Persistence.Setup.Infrastructure.Transactions
{
    public class EntityFrameworkTransactionManager : EntityFrameworkTransactionManager<DbContext>
    {
        public EntityFrameworkTransactionManager(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class EntityFrameworkTransactionManager<TDbContext> : ITransactionManager where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public EntityFrameworkTransactionManager(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITransaction Create()
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                return EntityFrameworkEmptyTransaction.Create(_dbContext.Database.CurrentTransaction);
            }

            return EntityFrameworkTransaction.Create(_dbContext, _dbContext.Database.BeginTransaction());
        }
    }
}
