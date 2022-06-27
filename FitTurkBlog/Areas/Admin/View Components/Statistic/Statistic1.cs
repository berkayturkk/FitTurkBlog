using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.Statistic
{
    public class Statistic1:ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.vBlogCount = blogManager.GetList().Count();
            ViewBag.vMessageCount = sqlDbContext.Contacts.Count();
            ViewBag.vCommentCount = sqlDbContext.Comments.Count(); 
            return View();
        }
    }
}
