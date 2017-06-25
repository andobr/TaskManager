using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Context;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Services
{
    public class TasksService
    {
        private readonly IDBContext context;
        private readonly TaskRepository taskRepository;
        private readonly CheckListItemRepository itemRepository;

        public TasksService(IConnectionFactory factory)
        {
            context = new DBContext(factory);
            taskRepository = new TaskRepository(context);
            itemRepository = new CheckListItemRepository(context);
        }

        public List<Task> GetAllTasks()
        {
            return taskRepository.Get().ToList();
        }

        public Task GetTask(int id)
        {
            var task = taskRepository.Get(id);
            return task;
        }

        public Task GetTaskDetails(int id)
        {
            var task = taskRepository.Get(id);
            if (task != null)
            {
                task.CheckList = itemRepository.GetByTaskId(task.Id).OrderBy(x => x.Order).ToList();
            }          
            return task;
        }

        public void CreateNewTask(Task task)
        {
            using (var uow = context.CreateUnitOfWork())
            {
                taskRepository.Create(task);
                uow.SaveChanges();
            }
        }

        public void EditTask(Task task)
        {
            using (var uow = context.CreateUnitOfWork())
            {
                taskRepository.Update(task);
                uow.SaveChanges();
            }
        }

        public void DeleteTask(int id)
        {
            using (var uow = context.CreateUnitOfWork())
            {
                taskRepository.Delete(id);
                uow.SaveChanges();
            }
        }

        private void UpdateTaskStatus(int taskId)
        {
            var taskItems = itemRepository.GetByTaskId(taskId).Select(x => x.Done);
            var task = taskRepository.Get(taskId);
            task.Status = taskItems.Contains(false) ? Status.Active : Status.Accomplished;
            taskRepository.Update(task);
        }

        public void CreateNewCheckListItem(CheckListItem item)
        {
            using (var uow = context.CreateUnitOfWork())
            {
                item.Done = false;
                itemRepository.Create(item);
                UpdateTaskStatus(item.TaskId);
                uow.SaveChanges();
            }
        }

        public void SetItemChecked(int itemId, bool state)
        {
            using (var uow = context.CreateUnitOfWork())
            {
                var item = itemRepository.Get(itemId);
                item.Done = state;
                itemRepository.Update(item);
                UpdateTaskStatus(item.TaskId);
                uow.SaveChanges();
            }
        }

        public void DeleteCheckListItem(int id)
        {
            var taskId = itemRepository.Get(id).TaskId;
            using (var uow = context.CreateUnitOfWork())
            {
                itemRepository.Delete(id);
                UpdateTaskStatus(taskId);
                uow.SaveChanges();
            }
        }
    }
}