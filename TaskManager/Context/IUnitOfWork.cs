using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Context 
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}