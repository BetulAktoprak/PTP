using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PTP.Business.Services;
using PTP.EntityLayer.Models;
using PTP.UI.Models;
using System.Security.Claims;

namespace PTP.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly IWebHostEnvironment _environment;
        private readonly PersonnelService _personnelService;
        private readonly DocumentService _documentService;

        public ProjectController(ProjectService projectService, IWebHostEnvironment environment, PersonnelService personnelService, DocumentService documentService)
        {
            _projectService = projectService;
            _environment = environment;
            _personnelService = personnelService;
            _documentService = documentService;
        }

        public IActionResult Index()
        {
            var values = _projectService.GetAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var personnels = _personnelService.GetAll();
            var model = new ProjectCreateViewModel
            {
                PersonnelList = personnels.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.FullName
                }).ToList()
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            var selectedPersonnels = _personnelService.GetAll()
                .Where(p => model.SelectedPersonnelIds.Contains(p.Id)).ToList();

            //var filePaths = new List<string>();

            var project = new Project
            {
                ProjectTitle = model.ProjectTitle,
                ClientName = model.ClientName,
                ProjectRate = model.ProjectRate,
                ProjectType = model.ProjectType,
                Priority = model.Priority,
                ProjectSize = model.ProjectSize,
                StartDate = model.StartingDate,
                EndDate = model.EndingDate,
                Details = model.Details,
                DocumentDetail = model.DocumentDetail,
                Personnels = selectedPersonnels,
            };

            _projectService.Add(project);



            if (model.ProjectFiles != null && model.ProjectFiles.Any())
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);

                foreach (var file in model.ProjectFiles)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var relativePath = Path.Combine("uploads", uniqueFileName);
                    var fullPath = Path.Combine(_environment.WebRootPath, relativePath);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Her dosya için Document kaydı oluştur
                    var document = new Document
                    {
                        FileName = file.FileName,
                        FilePath = relativePath,
                        ProjectId = project.Id,
                        UserId = userId // Oturumdaki kullanıcıyı al
                    };

                    _documentService.Add(document);
                }
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var project = _projectService.GetByID(id);
            if (project == null) return NotFound();

            var model = new ProjectCreateViewModel
            {
                Id = project.Id,
                ProjectTitle = project.ProjectTitle,
                ClientName = project.ClientName,
                ProjectRate = project.ProjectRate ?? 0,
                ProjectType = project.ProjectType,
                Priority = project.Priority,
                ProjectSize = project.ProjectSize,
                StartingDate = project.StartDate,
                EndingDate = project.EndDate ?? DateTime.Now,
                Details = project.Details,
                DocumentDetail = project.DocumentDetail,
                //ExistingFilePath = project.FilePath,
                SelectedPersonnelIds = project.Personnels?.Select(p => p.Id).ToList() ?? new List<int>(),
                PersonnelList = _personnelService.GetAll().Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.FullName
                }).ToList()
            };

            return PartialView("_EditProjectPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectCreateViewModel model)
        {
            var project = _projectService.GetByID(model.Id);
            if (project == null) return NotFound();

            var selectedPersonnels = _personnelService.GetAll()
                .Where(p => model.SelectedPersonnelIds.Contains(p.Id)).ToList();

            //string filePath = project.FilePath;
            //if (model.ProjectFile != null && model.ProjectFile.Length > 0)
            //{
            //    var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            //    Directory.CreateDirectory(uploads);

            //    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProjectFile.FileName);
            //    filePath = Path.Combine("uploads", uniqueFileName);

            //    using (var stream = new FileStream(Path.Combine(_environment.WebRootPath, filePath), FileMode.Create))
            //    {
            //        await model.ProjectFile.CopyToAsync(stream);
            //    }
            //}

            project.ProjectTitle = model.ProjectTitle;
            project.ClientName = model.ClientName;
            project.ProjectRate = model.ProjectRate;
            project.ProjectType = model.ProjectType;
            project.Priority = model.Priority;
            project.ProjectSize = model.ProjectSize;
            project.StartDate = model.StartingDate;
            project.EndDate = model.EndingDate;
            project.Details = model.Details;
            project.DocumentDetail = model.DocumentDetail;
            project.Personnels = selectedPersonnels;
            //project.FilePath = filePath;

            _projectService.Update(project);
            return Json(new { success = true });

        }
    }
}
