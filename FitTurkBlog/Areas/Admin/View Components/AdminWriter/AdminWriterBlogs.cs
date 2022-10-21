using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.AdminWriter
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminWriterBlogs : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        CommentManager commentManager = new CommentManager(new EFCommentRepository());
        public IViewComponentResult Invoke(int id)
        {
            var blogList = blogManager.GetBlogListWithCategoryWriter().Where(x => x.BlogWriterId == id).OrderByDescending(x => x.BlogCreateDate).ToList();

            return View(blogList);
        }
    }
}
