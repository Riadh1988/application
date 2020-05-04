using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Consume.Models;
using Consume.Helper;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Consume.Controllers
{
    public class ProjectsController : Controller
    {
        MyProjectAPI _api = new MyProjectAPI();
        // GET: Projects
         
        public async Task<ActionResult> GetProjects()
        {
            List<ProjectsData> Projects = new List<ProjectsData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/projects");

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Projects = JsonConvert.DeserializeObject<List<ProjectsData>>(results);
            }

            return View(Projects);

        }

        public async Task<ActionResult> Delete(Guid Id)
        {
            var Project = new ProjectsData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/projects/{Id}");

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Project = JsonConvert.DeserializeObject<ProjectsData>(results);


            }
            return RedirectToAction("GetProjects");

        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid Id)
        {
            var Project = new ProjectsData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/projects/{Id}");

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Project = JsonConvert.DeserializeObject<ProjectsData>(results);
            }

            return View(Project);

        }

        public ActionResult CreateProject()
        {
            return View();

        }

        [HttpPost]
        public ActionResult CreateProject(ProjectsData Project)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<ProjectsData>("api/Projects", Project);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetProjects");
            }
            return View();
        }



    }
}