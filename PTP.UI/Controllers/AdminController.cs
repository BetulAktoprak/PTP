using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTP.Business.Services;
using PTP.DataAccess;
using PTP.EntityLayer.Models;
using PTP.UI.Models;

namespace PTP.UI.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly ProcessService _processService;
        private readonly PersonnelService _personnelService;
        private readonly ProcessStageService _processStageService;

        public AdminController(ProjectService projectService, ProcessService processService, PersonnelService personnelService, ProcessStageService processStageService)
        {
            _projectService = projectService;
            _processService = processService;
            _personnelService = personnelService;
            _processStageService = processStageService;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageProcesses(int id)
        {
            var project = _projectService.GetByID(id);

            var viewModel = new ManageProcessViewModel
            {
                ProjectTitle = project.ProjectTitle
            };
            ViewBag.ProjectId = id;

            return View(viewModel);
        }

        //YENİ Kolon Ekleme
        [HttpPost]
        public IActionResult AddStage([FromBody] ProcessStage stage)
        {
            if (stage.ProjectId == null)
                return BadRequest("Proje ID gerekli");

            _processStageService.Add(stage);

            return Ok(stage.Id);
        }

        //YENİ Projeye göre kolonları getirme

        [HttpGet]
        public async Task<IActionResult> GetAllByProjectId(int id)
        {
            var processes = await _processService.GetAllByProjectIdAsync(id);
            var stages = await _processStageService.GetAllByProjectIdAsync(id);
            
            return Json(new
            {
                processes = processes.Select(p => new
                {
                    id = p.Id,
                    title = p.Title,
                    description = p.Description,
                    processType = p.ProcessStage.Name,
                    color = p.ProcessStage.ColorHex
                }),
                stages = stages.Select(s => new
                {
                    id = s.Id,
                    name = s.Name,
                    colorHex = s.ColorHex,
                })
            });
        }


        public class DeleteStageRequest
        {
            public int Id { get; set; }
        }

        [HttpPost]
        public IActionResult DeleteStage([FromBody] DeleteStageRequest request)
        {
            _processStageService.Delete(request.Id);
            return Ok();
        }






        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UpdateType([FromBody] ProcessUpdateViewModel dto)
        {
            var process = _processService.GetByID(dto.Id);
            if (process == null) return NotFound();

            process.ProcessStageId = dto.NewProcessStageId;

            process.UpdatedDate = DateTime.Now;
            var updatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            process.UpdatedBy = updatedBy ?? "Bilinmiyor";

            _processService.Update(process);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddProcess([FromBody] ProcessAddViewModel dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";

            var newProcess = new Process
            {
                Title = dto.Title,
                Description = dto.Description,
                ProjectId = dto.ProjectId,
                ProcessStageId = dto.ProcessStageId,
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
