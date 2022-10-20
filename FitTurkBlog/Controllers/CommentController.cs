using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        CommentManager _commentManager = new CommentManager(new EFCommentRepository());
        UserManager userManager = new UserManager(new EFUserRepository());
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
            var user = userManager.GetList().Where(x => x.NameSurname == comment.CommentUserName || x.UserName == comment.CommentUserName).FirstOrDefault();
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
