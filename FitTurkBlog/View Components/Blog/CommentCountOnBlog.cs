using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.View_Components.Blog
{
    public class CommentCountOnBlog : ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EFCommentRepository());
        public IViewComponentResult Invoke(int id)
        {
            var values = commentManager.CommentGetList(id);
            ViewBag.vCommentCount = values.Count;
            return View(values);
        }
    }
}
