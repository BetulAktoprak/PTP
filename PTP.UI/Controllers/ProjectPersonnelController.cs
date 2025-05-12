using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PTP.Business.Services;
using PTP.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace PTP.UI.Controllers
{
    public class ProjectPersonnelController : Controller
    {
        private readonly ProjectPersonnelService _projectPersonnelService;
        private readonly PersonnelService _personnelService;

        public ProjectPersonnelController(ProjectPersonnelService projectPersonnelService, PersonnelService personnelService)
        {
            _projectPersonnelService = projectPersonnelService;
            _personnelService = personnelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Personnel")]
        public async Task<IActionResult> PersonnelProject()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var projects = await _projectPersonnelService.GetProjectsByUserIdAsync(userId);

            if (projects == null || !projects.Any())
            {
                return NotFound("Atandığınız proje bulunamadı.");
            }

            return View(projects);
        }

        [Authorize(Roles = "Personnel")]
        [HttpGet]
        public async Task<IActionResult> PersonnelManageProcesses(int projectId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out int userId))
                return Unauthorized();

            var personnel = await _personnelService.GetPersonnelByUserIdAsync(userId);

            var projectPersonnel = await _projectPersonnelService.GetProjectPersonnelByUserIdAndProjectIdAsync(personnel.Id, projectId);

            if (projectPersonnel == null)
                return Forbid(); 

            if (projectPersonnel == null)
                return NotFound("Bu projeye atanmış yetkiniz bulunmamaktadır.");

            var viewModel = new ProjectPersonnelViewModel
            {
                PersonnelId = projectPersonnel.PersonnelId,
                ProjectId = projectPersonnel.ProjectId,
                CanRead = projectPersonnel.CanRead,
                CanCreate = projectPersonnel.CanCreate,
                CanUpdate = projectPersonnel.CanUpdate,
                CanDelete = projectPersonnel.CanDelete,
                CanComment = projectPersonnel.CanComment
            };

            return View(viewModel);
        }

    }
}
