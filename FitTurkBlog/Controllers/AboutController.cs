using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {

        AboutManager aboutManager = new AboutManager(new EFAboutRepository());
        public IActionResult Index()
        {
            SqlDbContext c = new SqlDbContext();
            ViewBag.v1 = c.Blogs.Count().ToString();
            ViewBag.v2 = c.Writers.Count().ToString();
            ViewBag.v3 = c.Comments.Count().ToString();
            ViewBag.v4 = c.Categories.Count().ToString();
            return View();
        }


        public PartialViewResult SocialMediaAbout()
        {         
            return PartialView();
        }

 

    }
}
