using Microsoft.AspNetCore.Mvc;
using PTP.Business.Services;

namespace PTP.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly ProcessService _processService;

        public AdminController(ProjectService projectService, ProcessService processService)
        {
            _projectService = projectService;
            _processService = processService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageProcesses()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var projects = _projectService.GetAll();
            return Json(projects.Select(p => new { id = p.Id, name = p.ProjectTitle }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByProjectId(int id)
        {
            var processes = await _processService.GetAllByProjectIdAsync(id);
            return Json(processes.Select(p => new
            {
                id = p.Id,
                title = p.Title,
                description = p.Description,
                processType = p.ProcessType
            }));
        }
        [HttpPost]
        public IActionResult UpdateType([FromBody] ProcessUpdateDto dto)
        {
            var process = _processService.GetByID(dto.Id);
            if (process == null) return NotFound();

            process.ProcessType = dto.NewType;
            _processService.Update(process);

            return Ok();
        }

        public class ProcessUpdateDto
        {
            public int Id { get; set; }
            public string NewType { get; set; }
        }

    }
}
