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
        private readonly ProcessService _processService;

        public ProjectController(ProjectService projectService, IWebHostEnvironment environment, PersonnelService personnelService, DocumentService documentService, UserService userService, ProjectPersonnelService projectPersonnelService, ProcessStageService processStageService, ProcessService processService)
        {
            _projectService = projectService;
            _environment = environment;
            _personnelService = personnelService;
            _documentService = documentService;
            _userService = userService;
            _projectPersonnelService = projectPersonnelService;
            _processStageService = processStageService;
            _processService = processService;
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

                var documents = _documentService.GetDocumentsByProjectId(project.Id)
                    .Select(d => new DocumentViewModel
                    {
                        Id = d.Id,
                        FileName = d.FileName,
                        FilePath = d.FilePath,
                        DocumentDescriptions = d.DocumentDescriptions
                    }).ToList();

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
                    ExistingDocuments = documents,
                    Stages = _processStageService.GetAll().Where(p => p.ProjectId == project.Id)
                                .Select(s => new ProcessStageViewModel
                                {
                                    ProcessStageName = s.Name,
                                    ColorHex = s.ColorHex
                                }).ToList()
                };

                //var existingDocuments = _documentService.GetAll()
                //    .Where(d => d.ProjectId == id.Value)
                //    .Select(d => new
                //    {
                //        FileName = d.FileName,
                //        FilePath = d.FilePath,
                //        Description = d.DocumentDescriptions
                //    }).ToList();

                //ViewBag.ExistingDocuments = JsonConvert.SerializeObject(existingDocuments);


                // Projeye atanmış personelleri ViewBag ile gönder
                var selectedPersonnel = _projectPersonnelService.GetAll()
                                            .Where(p => p.ProjectId == id)
                                            .Select(p => new ProjectPersonnelViewModel
                                            {
                                                Id = p.Id,
                                                ProjectId = p.ProjectId,
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
        public async Task<IActionResult> Create(ProjectCreateViewModel model, string SelectedPersonnelJson)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);


            //var descriptions = JsonConvert.DeserializeObject<List<string>>(DocumentDescriptionsJson);

            var files = Request.Form.Files;

            //if (files == null || descriptions == null)
            //{
            //    ModelState.AddModelError("", "Yüklenen dosyalar ile açıklamalar eşleşmiyor.");
            //    return View(model);
            //}

            try
            {
                Project project;

                if (model.ProjectId == 0)
                {
                    project = new Project
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
                if (model.ProjectId != 0)
                {
                    // Mevcut kolonları çek
                    var existingStages = _processStageService.GetAll()
                        .Where(x => x.ProjectId == project.Id)
                        .ToList();

                    // Gelen kolon listesi
                    var incomingStages = model.Stages;

                    // 1. Güncelleme veya ekleme işlemi
                    foreach (var incoming in incomingStages)
                    {
                        var existing = existingStages.FirstOrDefault(x => x.Name == incoming.ProcessStageName);

                        if (existing != null)
                        {
                            // Güncelle
                            existing.ColorHex = incoming.ColorHex;
                            existing.Order = incoming.Order;

                            _processStageService.Update(existing);
                        }
                        else
                        {
                            // Yeni kolon ise ekle
                            var newStage = new ProcessStage
                            {
                                Name = incoming.ProcessStageName,
                                ColorHex = incoming.ColorHex,
                                Order = incoming.Order,
                                ProjectId = project.Id
                            };

                            _processStageService.Add(newStage);
                        }
                    }

                    // 2. Silme işlemi: Artık gelen listede olmayan kolonları sil
                    var incomingOrders = incomingStages.Select(s => s.ProcessStageName).ToList();

                    foreach (var old in existingStages)
                    {
                        if (!incomingOrders.Contains(old.Name))
                        {
                            try
                            {
                                var processes = _processService.GetAll().Where(p => p.ProcessStageId == old.Id);
                                foreach (var process in processes)
                                {
                                    _processService.Delete(process.Id);
                                }
                                _processStageService.Delete(old.Id); // Eğer ilişkili işlem varsa burada hata verebilir
                            }
                            catch (Exception ex)
                            {
                                // soft delete’e dön veya logla
                                Console.WriteLine($"Silinemedi: StageId={old.Id} - {ex.Message}");
                            }
                        }
                    }
                }
                else
                {
                    // Yeni proje oluşturuluyorsa kolonları direkt ekle
                    foreach (var stage in model.Stages.OrderBy(s => s.Order))
                    {
                        var newStage = new ProcessStage
                        {
                            Name = stage.ProcessStageName,
                            ColorHex = stage.ColorHex,
                            Order = stage.Order,
                            ProjectId = project.Id
                        };

                        _processStageService.Add(newStage);
                    }
                }

                var docMeta = JsonConvert.DeserializeObject<List<DocumentViewModel>>(model.DocumentJson);
                var existingDocs = _documentService.GetDocumentsByProjectId(model.ProjectId);

                // Silinmiş dosyaları bul
                var existingIds = docMeta.Where(x => x.Id.HasValue).Select(x => x.Id.Value).ToList();
                var toDelete = existingDocs.Where(x => !existingIds.Contains(x.Id)).ToList();

                foreach (var doc in toDelete)
                {
                    var path = Path.Combine(_environment.WebRootPath, doc.FilePath);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    _documentService.Delete(doc.Id);
                }

                foreach (var item in docMeta.Where(x => x.Id.HasValue))
                {
                    var existing = existingDocs.FirstOrDefault(d => d.Id == item.Id);
                    if (existing != null && existing.DocumentDescriptions != item.DocumentDescriptions)
                    {
                        existing.DocumentDescriptions = item.DocumentDescriptions;
                        _documentService.Update(existing);
                    }
                }

                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);

                int newFileIndex = 0;
                foreach (var item in docMeta.Where(x => !x.Id.HasValue))
                {
                    var file = model.ProjectFiles[newFileIndex++];
                    var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var relativePath = Path.Combine("uploads", uniqueFileName);
                    var fullPath = Path.Combine(_environment.WebRootPath, relativePath);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var newDoc = new Document
                    {
                        FileName = file.FileName,
                        FilePath = relativePath,
                        DocumentDescriptions = item.DocumentDescriptions,
                        ProjectId = model.ProjectId,
                        UserId = userId
                    };

                    _documentService.Add(newDoc);
                }


                List<ProjectPersonnelViewModel> selectedList = new();
                if (!string.IsNullOrEmpty(SelectedPersonnelJson))
                {
                    selectedList = JsonConvert.DeserializeObject<List<ProjectPersonnelViewModel>>(SelectedPersonnelJson);
                }
                // Eski personel kayıtlarını sil


                foreach (var person in selectedList)
                {
                    var existing = _projectPersonnelService.GetAll()
                      .FirstOrDefault(p => p.ProjectId == project.Id && p.PersonnelId == person.PersonnelId);
                    if (existing != null)
                    {
                        _projectPersonnelService.Delete(existing.Id);
                    }

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



    }
    public class PersonnelTagModel
    {
        public string value { get; set; }
        public string name { get; set; }
    }
}
