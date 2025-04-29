using Microsoft.AspNetCore.Mvc;

namespace PTP.UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
