using FitTurkBlog.BL.Concrete;
using FitTurkBlog.BL.ValidationRules;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using X.PagedList;


namespace FitTurkBlog.UI.Controllers
{
    [Authorize(Roles = "Admin,Writer")]
    public class BlogController : Controller
    {
        BlogManager _blogManager = new BlogManager(new EFBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        CommentManager commentManager = new CommentManager(new EFCommentRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();


        [AllowAnonymous]
        // Bu metot ile ToPagedList ile sayfalama, OrderByDescending ile Bloglari yeniden eskiye gore BlogID lerine gore siralama islemleri yaptim.
        public IActionResult Index(int id,int page = 1)
        {
            var values = _blogManager.GetBlogListWithCategoryWriter().Where(x => x.BlogStatus == true).OrderByDescending(x => x.BlogCreateDate).ToPagedList(page, 6);
            var blogScores = commentManager.CommentGetList(id).Select(x => x.BlogScore).ToList();
            var totalBlogScore = ViewBag.vTotalBlogScore;

            ViewBag.vTotalBlogScore = totalBlogScore;
            return View(values);
        }

        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = _blogManager.GetBlogByID(id);
            return View(values);
        }

        public IActionResult BlogListByWriter(int page = 1)
        {
            var userName = User.Identity.Name;
            var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values = _blogManager.GetListWithCategoryByWriterBm(writerID).Where(x => x.BlogStatus == true).OrderByDescending(x => x.BlogCreateDate).ToPagedList(page, 6);
            return View(values);
        }

        public void CategorySelectList()
        {
            List<SelectListItem> categoryValue = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            List<SelectListItem> statusValue = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "True",
                    Value = "true"
                },
                new SelectListItem
                {
                    Text = "False",
                    Value = "false"
                }

            }.ToList();
            ViewBag.cv = categoryValue;
            ViewBag.sv = statusValue;
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            CategorySelectList();
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(AddBlogImage blogImageModel)
        {
            var userName = User.Identity.Name;
            var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();

            Blog blog = new Blog();

            if (blogImageModel.BlogImage != null)
            {
                var extension1 = Path.GetExtension(blogImageModel.BlogImage.FileName);
                var newImageName1 = Guid.NewGuid() + extension1;
                var location1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FitTurkBlog/newImages/", newImageName1);
                var stream1 = new FileStream(location1, FileMode.Create);
                blogImageModel.BlogImage.CopyTo(stream1);
                blog.BlogImage = "/FitTurkBlog/newImages/" + newImageName1;
                ViewBag.vBlogImage = "/FitTurkBlog/newImages/" + newImageName1;
            }
            if (blogImageModel.BlogThumbnailImage != null)
            {
                var extension2 = Path.GetExtension(blogImageModel.BlogThumbnailImage.FileName);
                var newImageName2 = Guid.NewGuid() + extension2;
                var location2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FitTurkBlog/newImages/", newImageName2);
                var stream2 = new FileStream(location2, FileMode.Create);
                blogImageModel.BlogThumbnailImage.CopyTo(stream2);
                blog.BlogThumbnailImage = "/FitTurkBlog/newImages/" + newImageName2;
                ViewBag.vBlogThumbnailImage = "/FitTurkBlog/newImages/" + newImageName2;
            }
            CategorySelectList();
            blog.CategoryID = blogImageModel.CategoryID;
            blog.BlogTitle = blogImageModel.BlogTitle;
            blog.BlogContent = blogImageModel.BlogContent;
            blog.BlogStatus = true;
            blog.BlogCreateDate = DateTime.Now;
            blog.BlogWriterId = writerID;
            _blogManager.Add(blog);
            return RedirectToAction("BlogListByWriter", "Blog");
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
            AddBlogImage blogImageModel = new AddBlogImage();
            TempData["blogID"] = blogValue.BlogID;
            List<SelectListItem> categoryValue = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            List<SelectListItem> statusValue = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "True",
                    Value = "true"
                },
                new SelectListItem
                {
                    Text = "False",
                    Value = "false"
                }

            }.ToList();

            blogImageModel.BlogID = blogValue.BlogID;
            blogImageModel.WriterID = blogValue.BlogWriterId;
            blogImageModel.BlogTitle = blogValue.BlogTitle;
            blogImageModel.BlogContent = blogValue.BlogContent;
            blogImageModel.CategoryID = blogValue.CategoryID;
            blogValue.BlogStatus = blogValue.BlogStatus;

            ViewBag.cv = categoryValue;
            ViewBag.sv = statusValue;
            return View(blogValue);
        }

        [HttpPost]
        public IActionResult EditBlog(AddBlogImage blogImageModel)
        {
            var userName = User.Identity.Name;
            var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();

            var blog = sqlDbContext.Blogs.Where(x => x.BlogID == Convert.ToInt32(TempData["blogID"])).FirstOrDefault();

            BlogValidator blogValidationRules = new BlogValidator();
            ValidationResult results = blogValidationRules.Validate(blog);
            if (results.IsValid)
            {
                if (blogImageModel.BlogImage != null)
                {
                    var extension1 = Path.GetExtension(blogImageModel.BlogImage.FileName);
                    var newImageName1 = Guid.NewGuid() + extension1;
                    var location1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FitTurkBlog/newImages/", newImageName1);
                    var stream1 = new FileStream(location1, FileMode.Create);
                    blogImageModel.BlogImage.CopyTo(stream1);
                    blog.BlogImage = "/FitTurkBlog/newImages/" + newImageName1;
                    ViewBag.vBlogImage = "/FitTurkBlog/newImages/" + newImageName1;
                }
                else if(blogImageModel.BlogThumbnailImage != null)
                {
                    var extension2 = Path.GetExtension(blogImageModel.BlogThumbnailImage.FileName);
                    var newImageName2 = Guid.NewGuid() + extension2;
                    var location2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FitTurkBlog/newImages/", newImageName2);
                    var stream2 = new FileStream(location2, FileMode.Create);
                    blogImageModel.BlogThumbnailImage.CopyTo(stream2);
                    blog.BlogThumbnailImage = "/FitTurkBlog/newImages/" + newImageName2;
                    ViewBag.vBlogThumbnailImage = "/FitTurkBlog/newImages/" + newImageName2;
                }
                blog.BlogWriterId = blogImageModel.WriterID;
                blog.BlogTitle = blogImageModel.BlogTitle;
                blog.BlogContent = blogImageModel.BlogContent;
                blog.BlogCreateDate = DateTime.Now;
                blog.BlogWriterId = writerID;
                blog.BlogID = Convert.ToInt32(TempData["blogID"]);
                _blogManager.Update(blog);
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



    }
}
