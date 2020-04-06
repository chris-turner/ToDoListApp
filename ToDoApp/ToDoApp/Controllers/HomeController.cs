using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ToDoApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ToDoDataAccess tda = new ToDoDataAccess();
            Guid userID = new Guid("b6fd0159-d33e-4322-a3b5-34db423d6620");
            ToDoList tdl = tda.getToDoTasks(userID);
            return View(tdl);
        }

        public IActionResult CompletedTasks()
        {
            ToDoDataAccess tda = new ToDoDataAccess();
            Guid userID = new Guid("b6fd0159-d33e-4322-a3b5-34db423d6620");
            ToDoList tdl = tda.getToDoTasksComplete(userID);
            return View(tdl);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult AddTask(string taskDesc, IFormFile file)
        {
            ToDoDataAccess tda = new ToDoDataAccess();
            string fileName = "";

            if (file != null)
            {
                fileName = file.FileName;

                tda.AddToDoTask(taskDesc, fileName);
                using (var stream = System.IO.File.Create($"wwwroot//" + file.FileName))
                {
                    file.CopyTo(stream);
                }
            }
            else
            {
                tda.AddToDoTask(taskDesc, fileName);
            }


            return Redirect("Index");
        }

        public IActionResult DeleteTask(Guid taskID)
        {
            ToDoDataAccess tda = new ToDoDataAccess();
            tda.DeleteToDoTask(taskID);
            return Redirect("Index");
        }

        public IActionResult DeleteTaskComplete(Guid taskID)
        {
            ToDoDataAccess tda = new ToDoDataAccess();
            tda.DeleteToDoTask(taskID);
            return Redirect("CompletedTasks");
        }

        public IActionResult UpdateTaskStatusComplete(Guid taskID)
        {
            ToDoDataAccess tda = new ToDoDataAccess();
            tda.updateTaskStatusComplete(taskID);
            return Redirect("Index");
        }

        public IActionResult UpdateTaskStatusPending(Guid taskID)
        {
            ToDoDataAccess tda = new ToDoDataAccess();
            tda.updateTaskStatusPending(taskID);
            return Redirect("CompletedTasks");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
