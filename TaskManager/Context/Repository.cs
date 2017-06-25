using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TaskManager.Context 
{
    public abstract class Repository<TEntity> where TEntity : new()
    {
        IDBContext _context;

        public Repository(IDBContext context)
        {
            _context = context;
        }

        protected DBContext Context { get; }

        protected IEnumerable<TEntity> ToList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                List<TEntity> items = new List<TEntity>();
                while (reader.Read())
                {
                    var item = new TEntity();
                    Map(reader, item);
                    items.Add(item);
                }
                return items;
            }
        }
        protected abstract void Map(IDataRecord record, TEntity entity);
    }
}