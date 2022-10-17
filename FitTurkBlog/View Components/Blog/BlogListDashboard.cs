using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.View_Components.Blog
{
    [Authorize(Roles = "Admin,Writer")]
    public class BlogListDashboard : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());

        public IViewComponentResult Invoke()
        {

            var values = blogManager.BlogGetLast10Blog().Where(x => x.BlogStatus == true).OrderByDescending(x => x.BlogCreateDate).ToList();
            return View(values);
        }
    }
}
