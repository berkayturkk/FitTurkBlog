using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminBlogController : Controller
    {
        BlogManager _blogManager = new BlogManager(new EFBlogRepository());

        public IActionResult Index(int page = 1)
        {
            var values = _blogManager.GetBlogListWithCategoryWriter().Where(x => x.BlogStatus == true).OrderByDescending(x => x.BlogID).ToPagedList(page, 8);
            return View(values);
        }

        public IActionResult ChangeStatusBlog(int id)
        {
            var value = _blogManager.TGetById(id);
            if (value.BlogStatus)
            {
                value.BlogStatus = false;
            }
            else
            {
                value.BlogStatus = true;
            }
            _blogManager.Update(value);

            return RedirectToAction("Index", "AdminWriter");
        }
    }
}
