using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PTP.Business.Services;
using PTP.EntityLayer.Models;
using PTP.UI.Models;
using System.Security.Claims;

namespace PTP.UI.Controllers
{
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();

        }
        [HttpGet]
        public JsonResult GetProjects()
        {
            var projects = _projectService.GetAll().Select(p => new
            {
                id = p.Id,
                projectTitle = p.ProjectTitle,
                clientName = p.ClientName,
                projectRate = p.ProjectRate,
                projectType = p.ProjectType,
                priority = p.Priority,
                projectSize = p.ProjectSize,
                startDate = p.StartDate.ToString("dd/MM/yyyy"),
                endDate = p.EndDate?.ToString("dd/MM/yyyy"),
                details = p.Details,
                documentDetail = p.DocumentDetail,
            });
            return Json(projects);
        }

        [Authorize(Roles = "Personel")]
        public IActionResult PersonnelProject()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var personnelList = _personnelService.GetAll();
            var values = personnelList.Select(p => new
            {
                value = p.Id.ToString(),
                name = p.FullName
            }).ToList();

            ViewBag.PersonnelListJson = JsonConvert.SerializeObject(values);

            return View();
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

            var selectedList = JsonConvert.DeserializeObject<List<PersonnelTagModel>>(model.SelectedPersonnelIds);

            foreach (var person in selectedList)
            {
                var personId = int.Parse(person.value);

            }
            var personnelIds = selectedList.Select(x => int.Parse(x.value)).ToList();
            var selectedPersonnels = _personnelService.GetAll()
                .Where(p => personnelIds.Contains(p.Id))
                .ToList();



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
                CreatedBy = userId.ToString()
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

                    var document = new Document
                    {
                        FileName = file.FileName,
                        FilePath = relativePath,
                        ProjectId = project.Id,
                        UserId = userId
                    };

                    _documentService.Add(document);
                }
            }


            return RedirectToAction("Index");
        }

        //[Authorize(Roles = "Admin")]
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var project = _projectService.GetByID(id);
        //    if (project == null) return NotFound();

        //    var model = new ProjectCreateViewModel
        //    {
        //        Id = project.Id,
        //        ProjectTitle = project.ProjectTitle,
        //        ClientName = project.ClientName,
        //        ProjectRate = project.ProjectRate ?? 0,
        //        ProjectType = project.ProjectType,
        //        Priority = project.Priority,
        //        ProjectSize = project.ProjectSize,
        //        StartingDate = project.StartDate,
        //        EndingDate = project.EndDate ?? DateTime.Now,
        //        Details = project.Details,
        //        DocumentDetail = project.DocumentDetail,
        //        SelectedPersonnelIds = project.Personnels?.Select(p => p.Id).ToList() ?? new List<int>(),
        //        PersonnelList = _personnelService.GetAll().Select(p => new SelectListItem
        //        {
        //            Value = p.Id.ToString(),
        //            Text = p.FullName
        //        }).ToList()
        //    };

        //    return PartialView("_EditProjectPartial", model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(ProjectCreateViewModel model)
        //{
        //    var project = _projectService.GetByID(model.Id);
        //    if (project == null) return NotFound();

        //    project.Personnels.Clear();

        //    var selectedPersonnels = _personnelService.GetAll()
        //        .Where(p => model.SelectedPersonnelIds.Contains(p.Id)).ToList();

        //    foreach (var person in selectedPersonnels)
        //    {
        //        project.Personnels.Add(person);
        //    }

        //    project.ProjectTitle = model.ProjectTitle;
        //    project.ClientName = model.ClientName;
        //    project.ProjectRate = model.ProjectRate;
        //    project.ProjectType = model.ProjectType;
        //    project.Priority = model.Priority;
        //    project.ProjectSize = model.ProjectSize;
        //    project.StartDate = model.StartingDate;
        //    project.EndDate = model.EndingDate;
        //    project.Details = model.Details;
        //    project.DocumentDetail = model.DocumentDetail;

        //    if (model.ProjectFiles != null && model.ProjectFiles.Any())
        //    {
        //        var uploads = Path.Combine(_environment.WebRootPath, "uploads");
        //        Directory.CreateDirectory(uploads);

        //        foreach (var file in model.ProjectFiles)
        //        {
        //            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //            var relativePath = Path.Combine("uploads", uniqueFileName);
        //            var fullPath = Path.Combine(_environment.WebRootPath, relativePath);

        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }

        //            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        //            int userId = int.Parse(userIdClaim.Value);

        //            var document = new Document
        //            {
        //                FileName = file.FileName,
        //                FilePath = relativePath,
        //                ProjectId = project.Id,
        //                UserId = userId
        //            };

        //            _documentService.Add(document);
        //        }
        //    }

        //    _projectService.Update(project);

        //    return Json(new { success = true });

        //}
    }
    public class PersonnelTagModel
    {
        public string value { get; set; }
        public string name { get; set; }
    }
}
