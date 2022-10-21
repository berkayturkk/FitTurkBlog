using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.View_Components.Blog
{
    [Authorize(Roles = "Admin,Writer")]
    public class WriterLastBlog : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        UserManager userManager = new UserManager(new EFUserRepository());

        public  IViewComponentResult Invoke(int id)
        {
            var userID = blogManager.GetBlogListWithCategoryWriter().Where(x => x.BlogID == id).Select(x => x.BlogWriterId).FirstOrDefault();
            var values = blogManager.GetBlogListByWriter(userID).Where(x => x.BlogStatus == true).OrderByDescending(x => x.BlogCreateDate).Take(10).ToList();
            return View(values);    
        }
    }
}
