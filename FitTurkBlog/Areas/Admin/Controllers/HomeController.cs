using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
