using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManager.Context;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    public class CheckListItemsController : Controller
    {
        private readonly TasksService taskService;

        public CheckListItemsController(IConnectionFactory factory)
        {
            taskService = new TasksService(factory);
        }

        public ActionResult Create(int taskId)
        {
            var item = new CheckListItem { TaskId = taskId, Done = false };
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Order,TaskId,Done")] CheckListItem item)
        {
            if (ModelState.IsValid)
            {
                taskService.CreateNewCheckListItem(item);
                return RedirectToAction("Details", "Tasks", new { id = item.TaskId });
            }
            return View(item);
        }

        [HttpPost]
        public bool ItemChecked(string itemId, string state)
        {
            var id = Convert.ToInt32(itemId);
            var stat = Convert.ToBoolean(state);
            taskService.SetItemChecked(id, stat);
            return true;
        }

        [HttpPost]
        public bool Delete(int id)
        {
            taskService.DeleteCheckListItem(id);
            return true;
        }
    }
}
