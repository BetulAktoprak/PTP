using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PTP.Business.Services;
using PTP.EntityLayer.Models;
using PTP.UI.Models;

namespace PTP.UI.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly IWebHostEnvironment _environment;
        private readonly PersonnelService _personnelService;

        public ProjectController(ProjectService projectService, IWebHostEnvironment environment, PersonnelService personnelService)
        {
            _projectService = projectService;
            _environment = environment;
            _personnelService = personnelService;
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

            var selectedPersonnels = _personnelService.GetAll()
                .Where(p => model.SelectedPersonnelIds.Contains(p.Id)).ToList();

            string filePath = null;

            if(model.ProjectFile != null &&  model.ProjectFile.Length > 0)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProjectFile.FileName);
                filePath = Path.Combine("uploads", uniqueFileName);

                using (var stream = new FileStream(Path.Combine(_environment.WebRootPath, filePath), FileMode.Create))
                {
                    await model.ProjectFile.CopyToAsync(stream);
                }
            }

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
                Personnels = selectedPersonnels,
                FilePath = filePath
            };

            _projectService.Add(project);
            return RedirectToAction("Index");
        }
    }
}
