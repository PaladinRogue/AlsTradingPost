using System;

namespace PaladinRogue.Library.Core.Application.Transactions
{
    public interface ITransaction : IDisposable
    {
	    void Commit();
	    void Rollback();
    }
}
