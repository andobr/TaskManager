using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.Context;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly TasksService taskService;

        public TasksController(IConnectionFactory factory)
        {
            taskService = new TasksService(factory);
        }

        public ActionResult Index()
        {
            var taskList = taskService.GetAllTasks();
            return View(taskList);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = taskService.GetTaskDetails(id.Value);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Status")] Task task)
        {
            if (ModelState.IsValid)
            {
                taskService.CreateNewTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = taskService.GetTask(id.Value); 
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Status")] Task task)
        {
            if (ModelState.IsValid)
            {
                taskService.EditTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = taskService.GetTask(id.Value); 
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            taskService.DeleteTask(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetTaskPartial(string Id)
        {
            var task = taskService.GetTask(Convert.ToInt32(Id));
            return PartialView("_taskPartialView", task);
        }
    }
}
