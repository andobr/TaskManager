using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TaskManager.Context;
using TaskManager.Models; 

namespace TaskManager.Repositories
{
    public class TaskRepository : Repository<Task>
    {
        private readonly IDBContext _connection;

        public TaskRepository(IDBContext context) : base(context)
        {
            _connection = context;
        }

        public Task Get(int id)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Task WHERE Id = @taskId";
                command.AddParameter("taskId", id);
                command.ExecuteNonQuery();

                return ToList(command).First();
            }           
        }

        public IEnumerable<Task> Get()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Task";
                return ToList(command);
            }
        }

        public void Create(Task task)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"INSERT INTO Task (Name, Description, Status) VALUES(@name, @description, @status)";
                command.AddParameter("name", task.Name);
                command.AddParameter("description", task.Description);
                command.AddParameter("status", task.Status);
                command.ExecuteNonQuery();
            }
        }

        public void Update(Task task)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"UPDATE Task SET Name = @name, Description = @description, Status = @status WHERE Id = @taskId";
                command.AddParameter("name", task.Name);
                command.AddParameter("description", task.Description);
                command.AddParameter("status", task.Status);
                command.AddParameter("taskId", task.Id);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"DELETE FROM Task WHERE Id = @taskId";
                command.AddParameter("taskId", id);
                command.ExecuteNonQuery();
            }
        }

        protected override void Map(IDataRecord record, Task task)
        {
            task.Id = (int)record["Id"];
            task.Name = (string)record["Name"];
            task.Description = (string)record["Description"];
            task.Status = (Status)record["Status"];
        }
    }
}