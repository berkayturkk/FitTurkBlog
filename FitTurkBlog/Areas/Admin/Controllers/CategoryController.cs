using ClosedXML.Excel;
using FitTurkBlog.BL.Concrete;
using FitTurkBlog.BL.ValidationRules;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Areas.Admin.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using X.PagedList;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();
        public IActionResult Index(string key,int page = 1)
        {
            if(key != null)
            {
                var values = categoryManager.GetListCategoryByKey(key).ToPagedList(page, 10);
                return View(values);
            }
            else
            {
                var values = categoryManager.GetList().ToPagedList(page, 10);
                return View(values);
            }
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

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var value = categoryManager.TGetById(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            category.CategoryStatus = true;
            categoryManager.Update(category);
            return RedirectToAction("Index");
        }


        public IActionResult CategoryDelete(int id)
        {
            var value = categoryManager.TGetById(id);
            categoryManager.Delete(value);
            return RedirectToAction("Index");
        }

        public IActionResult ChangeStatusCategory(int id)
        {
            var value = categoryManager.TGetById(id);
            if (value.CategoryStatus)
            {
                value.CategoryStatus = false;
            }
            else
            {
                value.CategoryStatus = true;
            }
            categoryManager.Update(value);

            return RedirectToAction("Index");
        }

        public IActionResult ExportDynamicExcelCategoryList()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Kategori Listesi");
                // Cell(Satır,Sütun)
                workSheet.Cell(1, 1).Value = "Kategori ID";
                workSheet.Cell(1, 2).Value = "Kategori Adı";

                int KategoriRowCount = 2;
                foreach (var item in CategoryTitleList())
                {
                    workSheet.Cell(KategoriRowCount, 1).Value = item.CategoryID;
                    workSheet.Cell(KategoriRowCount, 2).Value = item.CategoryName;
                    KategoriRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CategoryList.xlsx");
                }
            }
        }
        public List<CategoryViewModel> CategoryTitleList()
        {
            List<CategoryViewModel> categoryViewModels = new List<CategoryViewModel>();
            using (var sqlDbContext = new SqlDbContext())
            {
                categoryViewModels = sqlDbContext.Categories.Select(x => new CategoryViewModel
                {
                    CategoryID = x.CategoryID,
                    CategoryName = x.CategoryName
                }).ToList();
            }
            return categoryViewModels;
        }
    }
}
