using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTP.Business.Services;
using PTP.EntityLayer.Models;
using PTP.UI.Models;

namespace PTP.UI.Controllers
{
    public class PersonnelController : Controller
    {
        private readonly UserService _userService;
        private readonly PersonnelService _personnelService;
        private readonly ProcessService _processService;

        public PersonnelController(UserService userService, PersonnelService personnelService, ProcessService processService)
        {
            _userService = userService;
            _personnelService = personnelService;
            _processService = processService;
        }
        public IActionResult Index()
        {
            var personnels = _personnelService.GetAll();
            var viewModel = personnels.Select(p => new PersonelCreateViewModel
            {
                FullName = p.FullName,
                Email = p.Email,
                CroppedImage = p.ImagePath 
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> PersonelProcess(int id)
        {
            var processes = await _processService.GetAllByProjectIdAsync(id);
            if (processes == null || !processes.Any())
            {
                return NotFound("Proje Bulunamadı");

            }
            ViewBag.ProjectId = id;
            return View(processes);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PersonelCreateViewModel model)
        {
            var user = new User
            {
                Email = model.Email,
                Username = model.FullName,
                Password = model.Password,
                Role = model.Role
            };
            var result = await _userService.CreateUserAsync(user, model.Password!);

            var personnel = new Personnel
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                UserId = user.Id
            };

            if (!string.IsNullOrEmpty(model.CroppedImage))
            {
                var base64 = model.CroppedImage.Split(',')[1]; 
                var bytes = Convert.FromBase64String(base64);

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + ".png";
                var filePath = Path.Combine(uploadsFolder, fileName);

                await System.IO.File.WriteAllBytesAsync(filePath, bytes);

                personnel.ImagePath = "/uploads/" + fileName;
            }


            _personnelService.Add(personnel);

            return Json(new
            {
                id = personnel.Id,
                fullName = personnel.FullName,
                imagePath = personnel.ImagePath
            });
        }



    }
}
