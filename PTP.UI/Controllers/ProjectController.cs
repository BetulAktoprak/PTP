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
        private readonly UserService _userService;
        private readonly ProjectPersonnelService _projectPersonnelService;
        private readonly ProcessStageService _processStageService;

        public ProjectController(ProjectService projectService, IWebHostEnvironment environment, PersonnelService personnelService, DocumentService documentService, UserService userService, ProjectPersonnelService projectPersonnelService, ProcessStageService processStageService)
        {
            _projectService = projectService;
            _environment = environment;
            _personnelService = personnelService;
            _documentService = documentService;
            _userService = userService;
            _projectPersonnelService = projectPersonnelService;
            _processStageService = processStageService;
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
                Documents = _documentService.GetAll()
                       .Where(d => d.ProjectId == p.Id)
                       .Select(d => d.FilePath)
                       .ToList()
            });
            return Json(projects);
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create(int? id)
        {
            var userName = User.Identity.Name;
            ViewBag.UserName = userName;

            var personnelList = _personnelService.GetAll();
            var values = personnelList.Select(p => new
            {
                value = p.Id.ToString(),
                name = p.FullName
            }).ToList();

            ViewBag.PersonnelListJson = values;

            if (id is null)
            {
                return View(new ProjectCreateViewModel());
            }
            else
            {
                var project = _projectService.GetByID(id.Value);
                if (project == null)
                {
                    return NotFound();
                }

                var model = new ProjectCreateViewModel
                {
                    ProjectId = project.Id,
                    ProjectTitle = project.ProjectTitle,
                    ClientName = project.ClientName,
                    ProjectRate = Convert.ToDecimal(project.ProjectRate),
                    ProjectType = project.ProjectType,
                    Priority = project.Priority,
                    ProjectSize = project.ProjectSize,
                    StartingDate = project.StartDate,
                    EndingDate = Convert.ToDateTime(project.EndDate),
                    Details = project.Details,
                    ProcessStageName = project.ProcessStageName,
                    Stages = _processStageService.GetAll().Where(p => p.ProjectId == project.Id)
                                .Select(s => new ProcessStageViewModel
                                {
                                    ProcessStageName = s.Name,
                                    ColorHex = s.ColorHex
                                }).ToList()
                };

                var existingDocuments = _documentService.GetAll()
                    .Where(d => d.ProjectId == id.Value)
                    .Select(d => new
                    {
                        FileName = d.FileName,
                        FilePath = d.FilePath,
                        Description = d.DocumentDescriptions
                    }).ToList();

                ViewBag.ExistingDocuments = JsonConvert.SerializeObject(existingDocuments);


                // Projeye atanmış personelleri ViewBag ile gönder
                var selectedPersonnel = _projectPersonnelService.GetAll()
                                            .Where(p => p.ProjectId == id)
                                            .Select(p => new ProjectPersonnelViewModel
                                            {
                                                PersonnelId = p.PersonnelId,
                                                CanRead = p.CanRead,
                                                CanCreate = p.CanCreate,
                                                CanUpdate = p.CanUpdate,
                                                CanDelete = p.CanDelete,
                                                CanComment = p.CanComment
                                            }).ToList();

                ViewBag.SelectedPersonnel = JsonConvert.SerializeObject(selectedPersonnel);

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateViewModel model, string SelectedPersonnelJson, string DocumentDescriptionsJson)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);


            var descriptions = JsonConvert.DeserializeObject<List<string>>(DocumentDescriptionsJson);
            var files = Request.Form.Files;

            if (files == null || descriptions == null)
            {
                ModelState.AddModelError("", "Yüklenen dosyalar ile açıklamalar eşleşmiyor.");
                return View(model);
            }

            try
            {
                Project project;

                if (model.ProjectId == 0)
                {
                    project = new Project
                    {
                        ProcessStageName = model.ProcessStageName,
                        ProjectTitle = model.ProjectTitle,
                        ClientName = model.ClientName,
                        ProjectRate = model.ProjectRate,
                        ProjectType = model.ProjectType,
                        Priority = model.Priority,
                        ProjectSize = model.ProjectSize,
                        StartDate = model.StartingDate,
                        EndDate = model.EndingDate,
                        Details = model.Details,
                        CreatedBy = userId.ToString()
                    };

                    _projectService.Add(project);
                }
                else
                {
                    // Var olan projeyi güncelle
                    project = _projectService.GetByID(model.ProjectId);
                    if (project == null)
                    {
                        return NotFound();
                    }

                    project.ProcessStageName = model.ProcessStageName;
                    project.ProjectTitle = model.ProjectTitle;
                    project.ClientName = model.ClientName;
                    project.ProjectRate = model.ProjectRate;
                    project.ProjectType = model.ProjectType;
                    project.Priority = model.Priority;
                    project.ProjectSize = model.ProjectSize;
                    project.StartDate = model.StartingDate;
                    project.EndDate = model.EndingDate;
                    project.Details = model.Details;

                    _projectService.Update(project);


                }

                foreach (var stage in model.Stages)
                {
                    var newStage = new ProcessStage
                    {
                        Name = stage.ProcessStageName,
                        ColorHex = stage.ColorHex,
                        ProjectId = project.Id
                    };

                    _processStageService.Add(newStage);
                }

                //var defaultStages = new List<ProcessStage>
                //{
                //    new ProcessStage { Name = "ToDo", ColorHex = "#6c757d", ProjectId = project.Id },
                //    new ProcessStage { Name = "InProgress", ColorHex = "#ffc107", ProjectId = project.Id },
                //    new ProcessStage { Name = "Done", ColorHex = "#198754", ProjectId = project.Id }
                //};

                //_processStageService.AddRange(defaultStages);

                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);


                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[i];
                    var desc = descriptions[i];

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
                        DocumentDescriptions = desc,
                        ProjectId = project.Id,
                        UserId = userId
                    };

                    _documentService.Add(document);
                }

                List<ProjectPersonnelViewModel> selectedList = new();
                if (!string.IsNullOrEmpty(SelectedPersonnelJson))
                {
                    selectedList = JsonConvert.DeserializeObject<List<ProjectPersonnelViewModel>>(SelectedPersonnelJson);
                }

                foreach (var person in selectedList)
                {
                    var pp = new ProjectPersonnel
                    {
                        ProjectId = project.Id,
                        PersonnelId = person.PersonnelId,
                        CanRead = person.CanRead,
                        CanCreate = person.CanCreate,
                        CanUpdate = person.CanUpdate,
                        CanDelete = person.CanDelete,
                        CanComment = person.CanComment
                    };
                    _projectPersonnelService.Add(pp);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("HATA: " + ex.Message);
                ModelState.AddModelError("", "Bir hata oluştu, lütfen tekrar deneyin.");
                return View(model);
            }



            return RedirectToAction("Index");
        }


        //[Authorize(Roles = "Admin")]
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var project = _projectService.GetByID(id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new ProjectCreateViewModel
        //    {
        //        ProjectId = project.Id,
        //        ProjectTitle = project.ProjectTitle,
        //        ClientName = project.ClientName,
        //        ProjectRate = Convert.ToDecimal(project.ProjectRate),
        //        ProjectType = project.ProjectType,
        //        Priority = project.Priority,
        //        ProjectSize = project.ProjectSize,
        //        StartingDate = project.StartDate,
        //        EndingDate = Convert.ToDateTime(project.EndDate),
        //        Details = project.Details,
        //        ProcessStageName = project.ProcessStageName,
        //        Stages = _processStageService.GetAll().Where(p => p.ProjectId == project.Id)
        //                    .Select(s => new ProcessStageViewModel
        //                    {
        //                        ProcessStageName = s.Name,
        //                        ColorHex = s.ColorHex
        //                    }).ToList()
        //    };

        //    // Personel bilgilerini dropdown ya da seçilebilir formatta gönder
        //    var personnelList = _personnelService.GetAll();
        //    var values = personnelList.Select(p => new
        //    {
        //        value = p.Id.ToString(),
        //        name = p.FullName
        //    }).ToList();
        //    ViewBag.PersonnelListJson = values;

        //    // Projeye atanmış personelleri ViewBag ile gönder
        //    var selectedPersonnel = _projectPersonnelService.GetAll()
        //                                .Where(p => p.ProjectId == id)
        //                                .Select(p => new ProjectPersonnelViewModel
        //                                {
        //                                    PersonnelId = p.PersonnelId,
        //                                    CanRead = p.CanRead,
        //                                    CanCreate = p.CanCreate,
        //                                    CanUpdate = p.CanUpdate,
        //                                    CanDelete = p.CanDelete,
        //                                    CanComment = p.CanComment
        //                                }).ToList();

        //    ViewBag.SelectedPersonnel = JsonConvert.SerializeObject(selectedPersonnel);

        //    return View(model);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Edit(ProjectCreateViewModel model, string SelectedPersonnelJson, List<IFormFile> ProjectFiles, string DocumentDescriptionsJson)
        //{
        //    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        //    if (userIdClaim == null)
        //        return Unauthorized();

        //    if (!int.TryParse(userIdClaim.Value, out int userId))
        //        return Unauthorized();

        //    var project = _projectService.GetByID(model.ProjectId);
        //    if (project == null)
        //        return NotFound();

        //    var descriptions = JsonConvert.DeserializeObject<List<string>>(DocumentDescriptionsJson);
        //    var files = Request.Form.Files;

        //    if (files == null || descriptions == null || files.Count != descriptions.Count)
        //    {
        //        ModelState.AddModelError("", "Dosya ve açıklama sayıları uyuşmuyor.");
        //        return View(model);
        //    }

        //    try
        //    {
        //        // Proje güncelle
        //        project.ProjectTitle = model.ProjectTitle;
        //        project.ClientName = model.ClientName;
        //        project.ProjectRate = model.ProjectRate;
        //        project.ProjectType = model.ProjectType;
        //        project.Priority = model.Priority;
        //        project.ProjectSize = model.ProjectSize;
        //        project.StartDate = model.StartingDate;
        //        project.EndDate = model.EndingDate;
        //        project.Details = model.Details;
        //        project.ProcessStageName = model.ProcessStageName;

        //        _projectService.Update(project);

        //        // Eski sahneleri sil
        //        var existingStages = _processStageService.GetAll().Where(p => p.ProjectId == project.Id).ToList();
        //        foreach (var stage in existingStages)
        //        {
        //            _processStageService.Delete(stage.Id);
        //        }

        //        // Yeni sahneleri ekle
        //        foreach (var stage in model.Stages)
        //        {
        //            var newStage = new ProcessStage
        //            {
        //                Name = stage.ProcessStageName,
        //                ColorHex = stage.ColorHex,
        //                ProjectId = project.Id
        //            };
        //            _processStageService.Add(newStage);
        //        }

        //        // Dosya yükleme
        //        var uploads = Path.Combine(_environment.WebRootPath, "uploads");
        //        Directory.CreateDirectory(uploads);

        //        for (int i = 0; i < files.Count; i++)
        //        {
        //            var file = files[i];
        //            var desc = descriptions[i];

        //            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //            var relativePath = Path.Combine("uploads", uniqueFileName);
        //            var fullPath = Path.Combine(_environment.WebRootPath, relativePath);

        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }

        //            var document = new Document
        //            {
        //                FileName = file.FileName,
        //                FilePath = relativePath,
        //                DocumentDescriptions = desc,
        //                ProjectId = project.Id,
        //                UserId = userId
        //            };

        //            _documentService.Add(document);
        //        }

        //        // Önceki personelleri sil
        //        var existingPersonnel = _projectPersonnelService.GetAll().Where(p => p.ProjectId == project.Id).ToList();
        //        foreach (var p in existingPersonnel)
        //        {
        //            _projectPersonnelService.Delete(p);
        //        }

        //        // Yeni personelleri ekle
        //        List<ProjectPersonnelViewModel> selectedList = new();
        //        if (!string.IsNullOrEmpty(SelectedPersonnelJson))
        //        {
        //            selectedList = JsonConvert.DeserializeObject<List<ProjectPersonnelViewModel>>(SelectedPersonnelJson);
        //        }

        //        foreach (var person in selectedList)
        //        {
        //            var pp = new ProjectPersonnel
        //            {
        //                ProjectId = project.Id,
        //                PersonnelId = person.PersonnelId,
        //                CanRead = person.CanRead,
        //                CanCreate = person.CanCreate,
        //                CanUpdate = person.CanUpdate,
        //                CanDelete = person.CanDelete,
        //                CanComment = person.CanComment
        //            };
        //            _projectPersonnelService.Add(pp);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("HATA: " + ex.ToString());
        //        ModelState.AddModelError("", "Bir hata oluştu, lütfen tekrar deneyin.");
        //        return View(model);
        //    }

        //    return RedirectToAction("Index");
        //}

    }
    public class PersonnelTagModel
    {
        public string value { get; set; }
        public string name { get; set; }
    }
}
