using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTP.Business.Services;
using PTP.EntityLayer.Models;
using PTP.UI.Models;

namespace PTP.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly ProcessService _processService;
        private readonly PersonnelService _personnelService;

        public AdminController(ProjectService projectService, ProcessService processService, PersonnelService personnelService)
        {
            _projectService = projectService;
            _processService = processService;
            _personnelService = personnelService;
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
        public IActionResult UpdateType([FromBody] ProcessUpdateViewModel dto)
        {
            var process = _processService.GetByID(dto.Id);
            if (process == null) return NotFound();

            process.ProcessType = dto.NewType;

            process.UpdatedDate = DateTime.Now;
            var updatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            process.UpdatedBy = updatedBy ?? "Bilinmiyor";

            _processService.Update(process);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllPersonnel()
        {
            var users = _personnelService.GetAll()
                .Select(u => new
                {
                    id = u.Id,
                    fullName = u.FullName
                })
                .ToList();

            return Json(users);
        }

        [HttpPost]
        public IActionResult AddProcess([FromBody] ProcessAddViewModel dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";

            var newProcess = new Process
            {
                Title = dto.Title,
                Description = dto.Description,
                ProjectId = dto.ProjectId,
                ProcessType = dto.ProcessType,
                PersonnelId = dto.AssignedUserId,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                UpdatedBy = userId,
                UpdatedDate = DateTime.Now
            };

            _processService.Add(newProcess);

            return Ok();
        }



    }
}
