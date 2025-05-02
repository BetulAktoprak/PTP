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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PersonelCreateViewModel model)
        {

            var user = new User
            {
                Email = model.Email,
                Username = model.Email,
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

             _personnelService.Add(personnel);

            return Json(new { id = personnel.Id, fullName = personnel.FullName });
        }
    }
}
