﻿using FitTurkBlog.BL.Concrete;
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
            comment.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            comment.CommentStatus = true;
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
