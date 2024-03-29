﻿using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.AdminWriter
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminWriterComments : ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EFCommentRepository());
        public IViewComponentResult Invoke(int id)
        {
            var values = commentManager.GetListAllCommentByWriterWithBlog(id).OrderByDescending(x => x.CommentID).ToList();
            return View(values);
        }
    }
}
