using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.View_Components.About
{
    public class BlogLast3PostOnAbout : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());

        public IViewComponentResult Invoke()
        {

            var values = blogManager.BlogGetLast3BlogWithCategory();
            return View(values);
        }
    }
}
