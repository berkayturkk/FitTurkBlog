using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error1(int code)
        {
            return View();
        }
    }
}
