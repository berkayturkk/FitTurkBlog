using ClosedXML.Excel;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        public IActionResult ExportStaticExcelBlogList()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Blog Listesi");
                // Cell(Satır,Sütun)
                workSheet.Cell(1, 1).Value = "Blog ID";
                workSheet.Cell(1, 2).Value = "Blog Başlığı";

                int BlogRowCount = 2;
                foreach (var item in GetBlogList())
                {
                    workSheet.Cell(BlogRowCount, 1).Value = item.BlogModelID;
                    workSheet.Cell(BlogRowCount, 2).Value = item.BlogModelName;
                    BlogRowCount++;
                }
                using(var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calisma1.xlsx");
                }
            }
        }
        public List<BlogModel> GetBlogList()
        {
            List<BlogModel> blogModels = new List<BlogModel>
        {
            new BlogModel{BlogModelID = 1, BlogModelName = "Antreman Süresi Ne Kadar Olmalıdır ?"},
            new BlogModel{BlogModelID = 2, BlogModelName = "Protein Tozu Zararlı Mıdır ?"},
            new BlogModel{BlogModelID = 3, BlogModelName = "LISS Kardiyo Nedir ?"}
        };
            return blogModels;
        }

        public IActionResult BlogListExcel()
        {
            return View();
        }

        public IActionResult ExportDynamicExcelBlogList()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Blog Listesi");
                // Cell(Satır,Sütun)
                workSheet.Cell(1, 1).Value = "Blog ID";
                workSheet.Cell(1, 2).Value = "Blog Başlığı";

                int BlogRowCount = 2;
                foreach (var item in BlogTitleList())
                {
                    workSheet.Cell(BlogRowCount, 1).Value = item.BlogModel2ID;
                    workSheet.Cell(BlogRowCount, 2).Value = item.BlogModel2Name;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calisma1.xlsx");
                }
            }
        }
        public List<BlogModel2> BlogTitleList()
        {
            List<BlogModel2> blogModels2 = new List<BlogModel2>();
            using (var sqlDbContext = new SqlDbContext())
            {
                blogModels2 = sqlDbContext.Blogs.Select(x => new BlogModel2
                {
                    BlogModel2ID = x.BlogID,
                    BlogModel2Name = x.BlogTitle
                }).ToList();
            }
            return blogModels2;
        }

        public IActionResult BlogTitleListExcel()
        {
            return View();
        }
    }

}
