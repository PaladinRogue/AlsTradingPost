using System;

namespace Common.Setup.Infrastructure.Transactions
{
    public interface ITransaction : IDisposable
    {
	    void Commit();
	    void Rollback();
    }
}
