using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.View_Components.About
{
    [AllowAnonymous]
    public class BlogLast3PostOnAbout : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());

        public IViewComponentResult Invoke()
        {

            var values = blogManager.GetBlogListWithCategoryWriter().Where(x => x.BlogStatus == true).OrderByDescending(x => x.BlogCreateDate).Take(3).ToList();
            return View(values);
        }
    }
}
