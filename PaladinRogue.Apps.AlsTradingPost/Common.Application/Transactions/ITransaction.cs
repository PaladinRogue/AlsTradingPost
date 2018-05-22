using System;

namespace Common.Application.Transactions
{
    public interface ITransaction : IDisposable
    {
	    void Commit();
	    void Rollback();
    }
}
