using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCommentController : Controller
    {
        CommentManager _commentManager = new CommentManager(new EFCommentRepository());
        SqlDbContext _sqlDbContext = new SqlDbContext();
        public IActionResult Index(int page = 1)
        {
            var values = _commentManager.GetListAllComment().OrderByDescending(x => x.CommentID).ToPagedList(page, 4);
            return View(values);
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
