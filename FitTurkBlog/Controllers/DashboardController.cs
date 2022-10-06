using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Controllers
{
    public class DashboardController : Controller
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        public IActionResult Index()
        {
            SqlDbContext sqlDbContext = new SqlDbContext();
            var userName = User.Identity.Name;
            ViewBag.v = userName;
            var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerId = sqlDbContext.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();
            ViewBag.v1 = sqlDbContext.Blogs.Count().ToString();
            ViewBag.v2 = sqlDbContext.Blogs.Where(x => x.WriterID == writerId).Count().ToString();
            ViewBag.v3 = sqlDbContext.Categories.Count().ToString();
            return View();
        }

    }
}
