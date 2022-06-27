using FitTurkBlog.BL.Concrete;
using FitTurkBlog.BL.ValidationRules;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using X.PagedList;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class CategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();
        public IActionResult Index(int page = 1)
        {
            var values = categoryManager.GetList().ToPagedList(page,3);
            return View(values);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            CategoryValidator categoryValidationRules = new CategoryValidator();
            ValidationResult results = categoryValidationRules.Validate(category);
            if (results.IsValid)
            {
                category.CategoryStatus = true;
                categoryManager.Add(category);
                return RedirectToAction("Index", "Category");
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

        public IActionResult CategoryDelete(int id)
        {
            var value = categoryManager.TGetById(id);
            categoryManager.Delete(value);
            return RedirectToAction("Index");
        }
    }
}
