using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Context 
{
    public interface IDBContext : IDisposable
    {
        IUnitOfWork CreateUnitOfWork();

        IDbCommand CreateCommand();
    }
}
