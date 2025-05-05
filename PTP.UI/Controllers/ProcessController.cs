using Microsoft.AspNetCore.Mvc;
using PTP.Business.Services;
using PTP.EntityLayer.Models;

namespace PTP.UI.Controllers
{
    public class ProcessController : Controller
    {
        private readonly ProcessService _processService;

        public ProcessController(ProcessService processService)
        {
            _processService = processService;
        }

        //public IActionResult Index()
        //{
        //    var processes = _processService.GetAll();
        //    return View(processes);
        //}

        public async Task<IActionResult> Index(int projectId)
        {
            ViewBag.ProjectId = projectId;

            var processes = await _processService.GetAllByProjectIdAsync(projectId);
            return View(processes);
        }


        public async Task<IActionResult> KanbanBoard(int projectId)
        {
            var allProcess = await _processService.GetAllByProjectIdAsync(projectId);

            var viewModel = new Models.KanbanBoardViewModel
            {
                ProjectId = projectId,
                ToDo = allProcess.Where(p => p.ProcessType == "Todo").ToList(),
                Working = allProcess.Where(p => p.ProcessType == "Working").ToList(),
                Done = allProcess.Where(p => p.ProcessType == "Done").ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateProcessStatus(int processId, string newStatus)
        {
            var process = _processService.GetByID(processId);

            if (process is null) return NotFound();

            process.ProcessType = newStatus;
            _processService.Update(process);

            return Ok();
        }

        [HttpPost]
        public IActionResult Create(Process process)
        {
            if (ModelState.IsValid)
            {
                _processService.Add(process);
                return RedirectToAction("Index", new { projectId = process.ProjectId }); 
            }

            return RedirectToAction("Index", new { projectId = process.ProjectId });
        }


        //[HttpPost]
        //public IActionResult AddProcess([FromBody] Process process)
        //{
        //    _processService.Add(process);
        //    return Ok();
        //}

        //[HttpPost]
        //public IActionResult UpdateProcessStatus(int id, string status)
        //{
        //    var process = _processService.GetByID(id);
        //    if (process == null) return NotFound();

        //    process.ProcessType = status;
        //    _processService.Update(process);
        //    return Ok();
        //}
    }
}
