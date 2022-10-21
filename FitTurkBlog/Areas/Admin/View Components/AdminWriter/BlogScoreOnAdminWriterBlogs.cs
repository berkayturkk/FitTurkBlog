using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.AdminWriter
{
    public class BlogScoreOnAdminWriterBlogs : ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EFCommentRepository());
        public IViewComponentResult Invoke(int id)
        {
            var values = commentManager.CommentGetList(id).Select(x => x.BlogScore).ToList();
            var totalBlogScore = values.Sum();

            ViewBag.vTotalBlogScore = totalBlogScore;
            return View(values);
        }
    }
}
