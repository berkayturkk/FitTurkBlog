using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]  
    public class AdminBlogController : Controller
    {
        BlogManager _blogManager = new BlogManager(new EFBlogRepository());

        public IActionResult Index(int page = 1)
        {
            var values = _blogManager.GetBlogListWithCategoryWriter().Where(x => x.BlogStatus == true).OrderByDescending(x => x.BlogID).ToPagedList(page, 8);
            return View(values);
        }
    }
}
