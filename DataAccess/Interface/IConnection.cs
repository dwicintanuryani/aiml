using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interface
{
    public interface IConnection : IDisposable
    {
        void OpenConnection(string ConnString);
        string GetAppSettings(string key);

    }
}
