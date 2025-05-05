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

        public PersonnelController(UserService userService, PersonnelService personnelService)
        {
            _userService = userService;
            _personnelService = personnelService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PersonelCreateViewModel model, IFormFile ProfileImage)
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

            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await ProfileImage.CopyToAsync(stream);

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
