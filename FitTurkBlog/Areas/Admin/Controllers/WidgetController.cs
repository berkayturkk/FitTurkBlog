using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    public class WidgetController : Controller
    {
        [Area("Admin")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}
