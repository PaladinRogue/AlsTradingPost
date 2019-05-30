﻿using Common.Application.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.EntityFramework.Infrastructure.Transactions
{
    public class EntityFrameworkEmptyTransaction : ITransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction;

        private EntityFrameworkEmptyTransaction(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public static ITransaction Create(IDbContextTransaction dbContextTransaction)
        {
            return new EntityFrameworkEmptyTransaction(dbContextTransaction);
        }

        public void Dispose()
        {
            _dbContextTransaction.Dispose();
        }

        public void Commit()
        {
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }
    }
}
