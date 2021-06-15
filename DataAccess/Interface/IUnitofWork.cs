using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interface
{
    public interface IUnitofWork : IConnection, IDisposable
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
