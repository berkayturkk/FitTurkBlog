using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.AdminWriter
{
    public class CommentCountOnAdminWriterBlogs : ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EFCommentRepository());
        public IViewComponentResult Invoke(int id)
        {
            var commentList = commentManager.CommentGetList(id);

            var commentCount = commentList.Count;

            ViewBag.vCommentCount = commentCount;

            return View();
        }
    }
}
