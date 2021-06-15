using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interface
{
    public interface IVMRepository<T> where T : class
    {
        IList<T> ReadByQuery(string sqlQuery, object parameter);
    }
}
