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
            SqlDbContext sqlDbContext = new SqlDbContext();

            ViewBag.v1 = sqlDbContext.Blogs.Where(x => x.BlogStatus == true).Count().ToString();
            ViewBag.v2 = sqlDbContext.Users.Where(x => x.Status == true).Count().ToString();
            ViewBag.v3 = sqlDbContext.Comments.Where(x => x.CommentStatus == true).Count().ToString();
            ViewBag.v4 = sqlDbContext.Categories.Where(x => x.CategoryStatus == true).Count().ToString();
            return View();
        }


        public PartialViewResult SocialMediaAbout()
        {         
            return PartialView();
        }

 

    }
}
