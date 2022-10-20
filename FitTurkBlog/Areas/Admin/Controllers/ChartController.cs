using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ChartController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CategoryChart()
        {
            List<CategoryClass> list = new List<CategoryClass>();
            var categoryList = categoryManager.GetList().Where(x => x.CategoryStatus == true).ToList();
            foreach (var category in categoryList)
            {
                var blogCount = blogManager.GetBlogListWithCategory().Where(x => x.BlogStatus == true && x.CategoryID == category.CategoryID).ToList().Count;

                list.Add(new CategoryClass
                {
                    categoryname = category.CategoryName,
                    categorycount = blogCount
                });
            }

            
            return Json(new { jsonlist = list });
        }
    }
}
