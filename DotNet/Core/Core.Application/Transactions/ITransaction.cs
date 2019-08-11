using System;

namespace PaladinRogue.Libray.Core.Application.Transactions
{
    public interface ITransaction : IDisposable
    {
	    void Commit();
	    void Rollback();
    }
}
