using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCommentController : Controller
    {
        CommentManager _commentManager = new CommentManager(new EFCommentRepository());
        public IActionResult Index(string key,int page = 1)
        {
            if(key != null)
            {
                var values = _commentManager.GetListCommentByKey(key).OrderByDescending(x => x.CommentDate).ToPagedList(page, 10);
                return View(values);
            }
            else
            {
                var values = _commentManager.GetListAllComment().OrderByDescending(x => x.CommentDate).ToPagedList(page, 10);
                return View(values);
            }
        }

        public IActionResult DeleteComment(int id)
        {
            var value = _commentManager.GetCommentById(id);
            _commentManager.Delete(value);
            return RedirectToAction("Index", "AdminComment");
        }

        public IActionResult CommentDetails(int id)
        {
            var value = _commentManager.GetCommentById(id);
            return View(value);
        }
    }
}
