using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.Controllers
{
    public class AboutController : Controller
    {
        AboutManager aboutManager = new AboutManager(new EFAboutRepository());
        public IActionResult Index()
        {
            var values = aboutManager.GetList();
            return View(values);
        }

        public PartialViewResult SocialMediaAbout()
        {         
            return PartialView();
        }
    }
}
