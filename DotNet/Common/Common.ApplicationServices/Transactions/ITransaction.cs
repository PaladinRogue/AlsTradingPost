using System;

namespace Common.ApplicationServices.Transactions
{
    public interface ITransaction : IDisposable
    {
	    void Commit();
	    void Rollback();
    }
}
