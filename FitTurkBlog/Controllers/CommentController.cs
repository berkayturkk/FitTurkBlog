using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        UserManager _userManager = new UserManager(new EFUserRepository());
        CommentManager _commentManager = new CommentManager(new EFCommentRepository());

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PartialAddComment()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult PartialAddComment(Comment comment,int id)
        {
            var user = _userManager.GetList().FirstOrDefault(x => x.UserName == User.Identity.Name);
            comment.CommentDate = DateTime.Now;
            comment.CommentStatus = true;
            
            if(user != null)
            {
                comment.CommentImageUrl = user.ImageUrl;
            }
            else
            {
                comment.CommentImageUrl = "/FitTurkBlog/newImages/eedebfdc-b507-4a98-b5c3-2f9b37a1a32a.jpg";
            }

            _commentManager.CommentAdd(comment);
            return RedirectToAction("Index", "Blog");
        }

        public PartialViewResult CommentListByBlog(int id)
        {

            var values = _commentManager.CommentGetList(id);
            return PartialView(values);
        }

    }
}
