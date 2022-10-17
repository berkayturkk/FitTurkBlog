using FitTurkBlog.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.Statistic
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class Statistic2: ViewComponent 
    {
        SqlDbContext sqlDbContext = new SqlDbContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.vSonBlogTitle = sqlDbContext.Blogs.OrderByDescending(x => x.BlogID).Select(x => x.BlogTitle).Take(1).FirstOrDefault();
            ViewBag.vSonBlogContent = sqlDbContext.Blogs.OrderByDescending(x => x.BlogID).Select(x => x.BlogContent).Take(1).FirstOrDefault();
            ViewBag.vEarning = (sqlDbContext.Blogs.Count()*10)+(sqlDbContext.Users.Count()*5)+(sqlDbContext.Comments.Count()*1)+(sqlDbContext.Messages2.Count()*2);
            return View();
        }
    }
}
