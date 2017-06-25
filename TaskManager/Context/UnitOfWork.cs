using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TaskManager.Context 
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbTransaction _transaction;
        private readonly Action<UnitOfWork> _rolledBack;
        private readonly Action<UnitOfWork> _committed;

        public IDbTransaction Transaction { get; private set; }

        public UnitOfWork(IDbTransaction transaction, Action<UnitOfWork> rolledBack, Action<UnitOfWork> committed)
        {
            Transaction = transaction;
            _transaction = transaction;
            _rolledBack = rolledBack;
            _committed = committed;
        }        

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Невозможно сохранить изменения дважды.");
            _transaction.Commit();
            _committed(this);
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction == null)
                return;
            _transaction.Rollback();
            _transaction.Dispose();
            _rolledBack(this);
            _transaction = null;
        }
    }
}