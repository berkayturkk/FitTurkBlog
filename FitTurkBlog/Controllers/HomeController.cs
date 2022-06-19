using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
