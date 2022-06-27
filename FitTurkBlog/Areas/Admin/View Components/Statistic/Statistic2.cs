using FitTurkBlog.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.Statistic
{
    public class Statistic2: ViewComponent 
    {
        SqlDbContext sqlDbContext = new SqlDbContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.vSonBlogTitle = sqlDbContext.Blogs.OrderByDescending(x => x.BlogID).Select(x => x.BlogTitle).Take(1).FirstOrDefault();
            ViewBag.vSonBlogContent = sqlDbContext.Blogs.OrderByDescending(x => x.BlogID).Select(x => x.BlogContent).Take(1).FirstOrDefault();
            return View();
        }
    }
}
