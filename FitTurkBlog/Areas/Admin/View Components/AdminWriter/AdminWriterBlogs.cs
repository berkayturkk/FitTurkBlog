using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.AdminWriter
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminWriterBlogs : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        public IViewComponentResult Invoke(int id)
        {
            var values = blogManager.GetBlogListByWriter(id);
            return View(values);
        }
    }
}
