using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.View_Components.Blog
{
    [Authorize(Roles = "Admin,Writer")]
    public class WriterLastBlog : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());

        public IViewComponentResult Invoke()
        {
            
            var values = blogManager.GetBlogListByWriter(1);
            return View(values);    
        }
    }
}
