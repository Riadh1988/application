using Consume.Helper;
using Consume.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Consume.Controllers
{
    public class TasksController : Controller
    {
        MyProjectAPI _api = new MyProjectAPI();
        // GET: Tasks
        public async Task<ActionResult> Tasks(Guid Id)
        {
            List<TasksData> Tasks = new List<TasksData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/projects/{Id}/tasks");

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Tasks = JsonConvert.DeserializeObject<List<TasksData>>(results);
            }

            return View(Tasks);
        }

        [HttpGet]
        public async Task<ActionResult> TaskDetails(TasksData ProjectTSId, TasksData id)
        {
            var task = new TasksData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/projects/{ProjectTSId}/tasks/{id}");

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                task = JsonConvert.DeserializeObject<TasksData>(results);
            }

            return View(task);

        }
        public ActionResult CreateTask()
        {
            return View();

        }

         
        [HttpPost]
        public ActionResult CreateTask(TasksData task, Guid Project)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<TasksData>("api/Projects/{Project}/tasks", task);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Tasks", new { Id = task.ProjectTSId });
            }
            return View();
        }
    }
}