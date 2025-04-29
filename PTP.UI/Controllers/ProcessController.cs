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

        public IActionResult Index()
        {
            var processes = _processService.GetAll();
            return View(processes);
        }
        [HttpPost]
        public IActionResult AddProcess([FromBody] Process process)
        {
            _processService.Add(process);
            return Ok();
        }

        [HttpPost]
        public IActionResult UpdateProcessStatus(int id, string status)
        {
            var process = _processService.GetByID(id);
            if (process == null) return NotFound();

            process.ProcessType = status;
            _processService.Update(process);
            return Ok();
        }
    }
}
