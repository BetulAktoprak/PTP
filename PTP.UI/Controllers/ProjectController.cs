using Microsoft.AspNetCore.Mvc;
using PTP.Business.Services;

namespace PTP.UI.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public IActionResult Index()
        {
            var values = _projectService.GetAll();
            return View(values);
        }


    }
}
