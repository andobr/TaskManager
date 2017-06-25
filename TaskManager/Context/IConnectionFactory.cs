using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TaskManager.Context 
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}