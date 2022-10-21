using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.View_Components.Blog
{
    [AllowAnonymous]
    public class LastAddedBlog : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());

        public IViewComponentResult Invoke()
        {

            var values = blogManager.GetBlogListWithCategoryWriter().OrderByDescending(x => x.BlogCreateDate).Take(1).FirstOrDefault();
            return View(values);
        }
    }
}
