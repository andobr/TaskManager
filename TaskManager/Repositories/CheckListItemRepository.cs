using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TaskManager.Context;
using TaskManager.Models; 

namespace TaskManager.Repositories
{
    public class CheckListItemRepository : Repository<CheckListItem>
    {
        private readonly IDBContext _connection;

        public CheckListItemRepository(IDBContext context) : base(context)
        {
            _connection = context;
        }

        public CheckListItem Get(int id)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM CheckListItem WHERE Id = @itemId";
                command.AddParameter("itemId", id);
                command.ExecuteNonQuery();

                return ToList(command).FirstOrDefault();
            }           
        }

        public List<CheckListItem> GetByTaskId(int taskId)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM CheckListItem WHERE TaskId = @taskId";
                command.AddParameter("taskId", taskId);
                return ToList(command).ToList();
            }
        }

        public void Create(CheckListItem item)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"INSERT INTO CheckListItem (Name, Description, [Order], TaskId, Done) VALUES(@name, @description, @order, @taskId, @done)";
                command.AddParameter("name", item.Name);
                command.AddParameter("description", item.Description);
                command.AddParameter("order", item.Order);
                command.AddParameter("taskId", item.TaskId);
                command.AddParameter("done", Convert.ToString(item.Done));
                command.ExecuteNonQuery();
            }
        }

        public void Update(CheckListItem item)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"UPDATE CheckListItem SET Name = @name, Description = @description, [Order] = @order, Done = @done WHERE Id = @itemId";
                command.AddParameter("name", item.Name);
                command.AddParameter("description", item.Description);
                command.AddParameter("order", item.Order);
                command.AddParameter("itemId", item.Id);
                command.AddParameter("taskId", item.TaskId);
                command.AddParameter("done", Convert.ToString(item.Done));
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"DELETE FROM CheckListItem WHERE Id = @itemId";
                command.AddParameter("itemId", id);
                command.ExecuteNonQuery();
            }
        }

        protected override void Map(IDataRecord record, CheckListItem item)
        {
            item.Id = (int)record["Id"];
            item.Name = (string)record["Name"];
            item.Description = (string)record["Description"];
            item.Order = (int)record["Order"];
            item.TaskId = (int)record["TaskId"];
            item.Done = Convert.ToBoolean(record["Done"]);
        }
    }
}