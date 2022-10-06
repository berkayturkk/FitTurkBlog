using FitTurkBlog.BL.Concrete;
using FitTurkBlog.BL.ValidationRules;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;


namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        BlogManager _blogManager = new BlogManager(new EFBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();

        // Bu metot ile ToPagedList ile sayfalama, OrderByDescending ile Bloglari yeniden eskiye gore BlogID lerine gore siralama islemleri yaptim.
        public IActionResult Index(int page=1)
        {
            var values = _blogManager.GetBlogListWithCategory().OrderByDescending(x => x.BlogID).ToPagedList(page, 6);
            return View(values);
        }
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = _blogManager.GetBlogByID(id);
            return View(values);
        }

        public IActionResult BlogListByWriter()
        {
            var userMail = User.Identity.Name;
            var writerID = sqlDbContext.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();
            var values = _blogManager.GetListWithCategoryByWriterBm(writerID);
            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        { 
            List<SelectListItem> categoryValue = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.cv = categoryValue;
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog blog)
        {
            var userMail = User.Identity.Name;
            var writerID = sqlDbContext.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();
            BlogValidator blogValidationRules = new BlogValidator();
            ValidationResult results = blogValidationRules.Validate(blog);
            if (results.IsValid)
            {
                blog.BlogStatus = true;
                blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.WriterID = writerID;
                _blogManager.Add(blog);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public IActionResult DeleteBlog(int id)
        {
            var blogValue = _blogManager.TGetById(id);
            _blogManager.Delete(blogValue);
            return RedirectToAction("BlogListByWriter");
        }

        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogValue = _blogManager.TGetById(id);
            List<SelectListItem> categoryValue = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.cv = categoryValue;
            return View(blogValue);
        }

        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            var userMail = User.Identity.Name;
            var writerID = sqlDbContext.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();
            blog.WriterID = writerID;
            blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            blog.BlogStatus = true;
            _blogManager.Update(blog);
            return RedirectToAction("BlogListByWriter");
        }


    }
}
