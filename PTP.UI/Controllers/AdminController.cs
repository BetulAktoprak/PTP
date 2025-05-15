using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PTP.Business.Abstractions;
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
        private readonly DocumentService _documentService;
        private readonly CommentService _commentService;
        private readonly ProjectPersonnelService _projectPersonnelService;

        public AdminController(ProjectService projectService, ProcessService processService, PersonnelService personnelService, ProcessStageService processStageService, DocumentService documentService, CommentService commentService, ProjectPersonnelService projectPersonnelService)
        {
            _projectService = projectService;
            _processService = processService;
            _personnelService = personnelService;
            _processStageService = processStageService;
            _documentService = documentService;
            _commentService = commentService;
            _projectPersonnelService = projectPersonnelService;
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
        //[HttpPost]
        //public IActionResult AddStage([FromBody] ProcessStage stage)
        //{
        //    if (stage.ProjectId == null)
        //        return BadRequest("Proje ID gerekli");

        //    _processStageService.Add(stage);

        //    return Ok(stage.Id);
        //}

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
        [HttpGet]
        public IActionResult AddProcess(int projectId, int? processId = null)
        {
            var personnels = _projectPersonnelService.GetAllWithPersonnelByProjectId(projectId);

            var model = new ProcessAddViewModel
            {
                ProjectId = projectId,
                ProjectPersonnels = personnels
                    .Where(x => x.Personnel != null)
                    .Select(x => new SelectListItem
                    {
                        Value = x.PersonnelId.ToString(),
                        Text = x.Personnel.FullName
                    }).ToList()
            };


            return PartialView("_AddProcessPartial", model); // modal için partial kullanıyorsan
        }

        
        [HttpPost]
        public async Task<IActionResult> AddProcess([FromForm] ProcessAddViewModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";

            var project = _projectService.GetByID(model.ProjectId);
            if (project == null)
            {
                return NotFound("Proje bulunamadı.");
            }

            var newProcess = new Process
            {
                Title = model.Title,
                Description = model.Description,
                ProjectId = model.ProjectId,
                ProcessStageId = model.ProcessStageId,
                PersonnelId = model.AssignedUserId,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                UpdatedBy = userId,
                UpdatedDate = DateTime.Now
            };

            _processService.Add(newProcess);

            if (model.Files != null && model.Files.Any())
            {
                foreach (var file in model.Files)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var document = new EntityLayer.Models.Document
                    {
                        FileName = fileName,
                        FilePath = "/uploads/" + fileName,
                        ProcessId = newProcess.Id,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                        ProjectId = model.ProjectId,
                        DocumentDescriptions = model.DocumentDescriptions
                    };

                    _documentService.Add(document);
                }
            }

            if (!string.IsNullOrWhiteSpace(model.Text) && model.PersonnelId > 0)
            {
                var comment = new Comment
                {
                    Text = model.Text,
                    ProcessId = model.ProcessId,
                    PersonnelId = model.PersonnelId,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now
                };

                _commentService.Add(comment);
            }

            return Ok();
        }



    }
}
